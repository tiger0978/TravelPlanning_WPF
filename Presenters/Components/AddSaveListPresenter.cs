using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using TravelPlanning.Contracts;
using TravelPlanning.Models.DTOs;
using TravelPlanning.Models.Entities;
using TravelPlanning.Respositories;
using TravelPlanning.Respositories.Models.DAOs;

namespace TravelPlanning.Presenters.Components
{
    public class AddSaveListPresenter : IAddSaveListPresenter
    {
        private readonly IMapPlaceRepository _mapPlaceRepository;
        private readonly IMapLayerRepository _mapLayerRepository;
        private readonly IAddSaveListComponentView _addSaveListView;
        private readonly IGoogleAPIContext _googleAPIContext;
        public AddSaveListPresenter(IMapPlaceRepository mapPlaceRepository,IMapLayerRepository mapLayerRepository,
            IAddSaveListComponentView addSaveListView, IGoogleAPIContext googleAPIContext) 
        {
            _mapPlaceRepository = mapPlaceRepository;
            _mapLayerRepository = mapLayerRepository;
            _addSaveListView = addSaveListView;
            _googleAPIContext = googleAPIContext;
        }

        public async void GetMapPlaces(Guid mapLayerId)
        {
            var places = await _mapPlaceRepository.GetMapPlacesByMapperIdAsync(mapLayerId);
            var placeDtos = places.Select(async x =>
            {
                var res = await _googleAPIContext.Place.PlaceDetailAsync(x.PlaceId);
                // 取得照片
                var bytes = await _googleAPIContext.Place.PlacePhotoAsync(res.result.photos[0].photo_reference, 450);
                var image = new BitmapImage();
                using (var ms = new MemoryStream(bytes))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad; // 很重要
                    image.StreamSource = ms;
                    image.EndInit();
                    image.Freeze(); // 跨執行緒安全
                }

                return new SavePlaceDTO
                {
                    Id = x.Id,
                    MapLayerId = x.MapLayerId,
                    PlaceId = x.PlaceId,
                    Name = x.Name,
                    Type = res.result.types?[0],
                    Rate = res.result.rating,
                    Photo = image
                };
            });
            var result = await Task.WhenAll(placeDtos);
            _addSaveListView.RenderList(result.ToList());
        }

        public async void UpdateListName(Guid mapLayerId, string name)
        {
            await _mapLayerRepository.UpdateNameAsync(mapLayerId, name);
        }

        public async void AddPlace(AddPlaceInputDTO placeDTO)
        {
            var placeDAO = new MapPlaceDAO(placeDTO.MapLayerId, placeDTO.PlaceId, placeDTO.PlaceName);
            await _mapPlaceRepository.AddMapPlaceAsync(placeDAO);
            GetMapPlaces(placeDTO.MapLayerId);
        }
    }
}
