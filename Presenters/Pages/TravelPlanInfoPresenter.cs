using GoogleMap.SDK.Contracts.Commons.Enums;
using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TravelPlanning.Contracts;
using TravelPlanning.Models.Entities;
using TravelPlanning.Respositories;
using TravelPlanning.Respositories.Models.DAOs;
using TravelPlanning.Views.Pages.TravelPlanInfo.Models;
using TravelPlanDTO = TravelPlanning.Views.Pages.TravelPlanInfo.Models.TravelPlanDTO;

namespace TravelPlanning.Presenters.Pages
{
    public class TravelPlanInfoPresenter : ITravelPlanInfoPresenter
    {
        private readonly ITravelRepository _travelRepository;
        private readonly ITravelPlanDayRepository _travelPlanDayRepository;
        private readonly ITravelPlanInfoView _planInfoView;
        private readonly IGoogleAPIContext _googleAPIContext;
        public TravelPlanInfoPresenter(ITravelPlanInfoView planInfoView, 
            ITravelRepository travelRepository, 
            ITravelPlanDayRepository travelPlanDayRepository,
            IGoogleAPIContext googleAPIContext) 
        {
            _travelRepository = travelRepository;
            _travelPlanDayRepository = travelPlanDayRepository;
            _planInfoView = planInfoView;
            _googleAPIContext = googleAPIContext;
        }

        public async Task AddNewPlace(AddTravelPlaceDTO addTravelPlaceDTO)
        {
            var travelPlaceDAO = Mapper.Map<AddTravelPlaceDTO, TravelPlaceDAO>(addTravelPlaceDTO);
            await _travelRepository.AddTravelPlaceAsync(travelPlaceDAO);
            var travelPlacesDao = await UpdateTravelPlaceTrafficInfo(addTravelPlaceDTO.TravelDayId);
            var arrangedPlacesDto = await GetArrangedTravelPlaces(travelPlacesDao);
            _planInfoView.OnAddTravelPlaceResponse(arrangedPlacesDto);
        }

        public async Task DeletePlace(DeleteTravelPlaceDTO input)
        {
            await _travelRepository.DeleteTravelPlaceByIdAsync(input.TravelPlaceId);
            var travelPlacesDao = await UpdateTravelPlaceTrafficInfo(input.TravelDayId);
            var arrangedPlacesDto = await GetArrangedTravelPlaces(travelPlacesDao);
            _planInfoView.OnDeleteTravelPlaceResponse(arrangedPlacesDto);
        }

        public async Task GetTravelPlanInfo(Guid planId)
        {
            var plan = await _travelRepository.GetTravelPlanByIdAsync(planId);
            var planDto = new TravelPlanDTO
            {
                TravelDays = (await Task.WhenAll(plan.TravelDays.OrderBy(x=>x.DayOrder).Select(async x =>
                {
                    var places = await ConvertToTravelPlaceDTO(x.TravelPlaces);
                    return new TravelPlanDayDTO
                    {
                        Id = x.Id,
                        TravelDate = x.TravelDate,
                        DayName = $"第{x.DayOrder}天",
                        TravelPlaces = places.ToList()
                    };
                }))).ToList()
            };
            planDto.TravelDays = planDto.TravelDays.Select(x =>
            {
                x.TravelPlaces.Aggregate(new List<TravelPlaceDTO>(), (travels, travel) =>
                {
                    if (travels.Count == 0)
                    {
                        return new List<TravelPlaceDTO>() { travel };
                    }
                    var lastTravel = travels.Last();
                    if (!travel.HasArrivedTime)
                    {
                        travel.LastPlaceLeavingTime = lastTravel.TravelTime.AddMinutes(lastTravel.Duration);
                        travel.TravelTime = travel.LastPlaceLeavingTime.Value.AddMinutes(travel.TrafficDuration);
                    }
                    travels.Add(travel);
                    return travels;
                });
                return x;
            }).ToList();
            _planInfoView.OnTravelPlanInfoResponse(planDto);
        }

        public async Task UpdateTravelTime(TravelPlaceDTO travelPlaceDTO)
        {
            var placeDAO = Mapper.Map<TravelPlaceDTO, TravelPlaceDAO>(travelPlaceDTO);
            await _travelRepository.UpdateTravelPlaceTimeByIdAsync(placeDAO);
            var travelPlacesDao = await UpdateTravelPlaceTrafficInfo(travelPlaceDTO.TravelDayId);
            var arrangedPlacesDto = await GetArrangedTravelPlaces(travelPlacesDao);
            _planInfoView.OnEditTravelPlaceTimeResponse(arrangedPlacesDto);
        }

        public async Task UpdateTravelWay(TravelPlaceDTO travelPlaceDTO)
        {
            var travelPlacesDao = await _travelRepository.GetTravelPlacesByTravelDayId(travelPlaceDTO.TravelDayId);
            var index = travelPlacesDao.FindIndex(x => x.Id == travelPlaceDTO.Id);
            var resp = await _googleAPIContext.Direction.GetDirectionAsync(travelPlacesDao[index].PlaceId, travelPlacesDao[index-1].PlaceId, travelPlaceDTO.TrafficType, new List<Avoid> { });
            travelPlacesDao[index].TrafficDuration = int.Parse(resp.routes[0].legs[0].duration.Replace("s", "").Trim()) / 60;
            travelPlacesDao[index].TrafficType = travelPlaceDTO.TrafficType;
            travelPlacesDao = await _travelRepository.UpdateTrafficAndRecalculateTimeAsync(travelPlaceDTO.TravelDayId, travelPlacesDao);
            var arrangedPlacesDto = await GetArrangedTravelPlaces(travelPlacesDao);
            _planInfoView.OnEditTravelPlaceTimeResponse(arrangedPlacesDto);
        }

        public async Task AddNewTravelDay(Guid travelPlanId)
        {
            var travelDayDAO = await _travelPlanDayRepository.AddNewTravelDay(travelPlanId);
            var travelDayDTO = new TravelPlanDayDTO
            {
                Id = travelDayDAO.Id,
                DayName = $"第{travelDayDAO.DayOrder}天",
                TravelDate = travelDayDAO.TravelDate,
            };
            _planInfoView.OnAddNewTravelDay(travelDayDTO);
        }

        private async Task<List<TravelPlaceDAO>> UpdateTravelPlaceTrafficInfo(Guid travelDayId) 
        {
            var travelPlacesDao = await _travelRepository.GetTravelPlacesByTravelDayId(travelDayId);
            if (travelPlacesDao.Count > 1)
            {
                for(int i = 0; i< travelPlacesDao.Count-1; i++) 
                {
                    var resp = await _googleAPIContext.Direction.GetDirectionAsync(travelPlacesDao[i].PlaceId, travelPlacesDao[i+1].PlaceId, travelPlacesDao[i + 1].TrafficType, new List<Avoid> { });
                    travelPlacesDao[i + 1].TrafficDuration = int.Parse(resp.routes[0].legs[0].duration.Replace("s", "").Trim()) / 60;
                }
                travelPlacesDao = await _travelRepository.UpdateTrafficAndRecalculateTimeAsync(travelDayId, travelPlacesDao);
            }
            return travelPlacesDao;
        }

        private async Task<List<TravelPlaceDTO>> GetArrangedTravelPlaces(List<TravelPlaceDAO> travelPlaceDaos) 
        {
            var placesDto = await ConvertToTravelPlaceDTO(travelPlaceDaos);
            return placesDto.ToList();
        }

        private async Task<IEnumerable<TravelPlaceDTO>> ConvertToTravelPlaceDTO(List<TravelPlaceDAO> travelPlaceDAOs) 
        {
            return await Task.WhenAll(travelPlaceDAOs
            .OrderBy(y => y.TravelTime)
            .Select(async x => new TravelPlaceDTO
            {
                Id = x.Id,
                Duration = x.Duration,
                TravelDayId = x.TravelDayId,
                Name = x.Name,
                PlaceId = x.PlaceId,
                TravelTime = x.TravelTime,
                HasArrivedTime = x.HasArrivedTime,
                Cover = ToBitmapImage(await _googleAPIContext.Place.PlacePhotoAsync(
                                    (await _googleAPIContext.Place.PlaceDetailAsync(x.PlaceId))
                                    .result.photos[0].photo_reference, 100)),
                TrafficDuration = x.TrafficDuration,
                TrafficType = x.TrafficType,
                LastPlaceLeavingTime = x.LastPlaceLeavingTime.HasValue ? x.LastPlaceLeavingTime.Value : x.LastPlaceLeavingTime
            }));
        }
        private BitmapImage ToBitmapImage(Byte[] bytes) 
        {
            var image = new BitmapImage();
            using (var ms = new MemoryStream(bytes))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // 很重要
                image.StreamSource = ms;
                image.EndInit();
                image.Freeze(); // 跨執行緒安全
            }
            return image;
        }


    }
}
