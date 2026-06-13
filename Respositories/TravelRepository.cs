using GoogleMap.SDK.Contracts.Commons.Enums;
using GoogleMap.SDK.Core.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TravelPlanning.Models.Entities;
using TravelPlanning.Respositories.Models.DAOs;
using TravelPlanning.Respositories.Models.Entities;

namespace TravelPlanning.Respositories
{
    public class TravelRepository : ITravelRepository
    {
        private readonly DatabaseContext _db;

        public TravelRepository(DatabaseContext db)
        {
            _db = db;
        }
        public async Task<TravelPlaceDAO> AddTravelPlaceAsync(TravelPlaceDAO travelPlace)
        {
            var entity = Mapper.Map<TravelPlaceDAO, TravelPlace>(travelPlace, config => 
            {
                config
                .ForMember(dest => dest.Id, source => source.MapFrom(x => Guid.NewGuid()));
            });
            _db.TravelPlaces.Add(entity);
            await _db.SaveChangesAsync();
            var placeDAO = new TravelPlaceDAO()
            {
                Id = entity.Id,
                Duration = entity.Duration,
                Name = entity.Name,
                PlaceId = entity.PlaceId,
                TravelDayId = entity.TravelDayId,
                TravelTime = entity.TravelTime,
                TrafficDuration = entity.TrafficDuration ?? 0,
                TrafficType = entity.TrafficType == null ? TrafficMode.DRIVE : (TrafficMode)entity.TrafficType
            };
            return placeDAO;
        }
        public async Task<TravelPlanDAO> AddTravelPlanAsync(TravelPlanDAO travelPlan)
        {
            var entity = Mapper.Map<TravelPlanDAO,TravelPlan>(travelPlan);
            _db.TravelPlans.Add(entity);
            await _db.SaveChangesAsync();
            var result = Mapper.Map<TravelPlan, TravelPlanDAO>(entity);
            return result;
        }
        public async Task DeleteTravelPlaceByIdAsync(Guid placeId)
        {
            var entity = await _db.TravelPlaces.FirstOrDefaultAsync(x => x.Id == placeId);
            if (entity != null) 
            {
                _db.TravelPlaces.Remove(entity);
            }
            await _db.SaveChangesAsync();
        }
        public async Task<bool> DeleteTravelPlanByIdAsync(Guid id)
        {
            var plan = await _db.TravelPlans.FirstOrDefaultAsync(x => x.Id == id);
            _db.TravelPlans.Remove(plan);
            var result = await _db.SaveChangesAsync();
            return result != 0; 
        }
        public async Task<List<TravelPlaceDAO>> GetTravelPlacesAsync(Guid travelPlanId)
        {
            var plan = await _db.TravelPlans.FirstOrDefaultAsync(x => x.Id == travelPlanId);
            var places = plan.TravelDays.SelectMany(x => x.TravelPlaces).ToList();
            var result = Mapper.Map<TravelPlace, TravelPlaceDAO>(places).ToList();
            return result;
        }

        public async Task<TravelPlanDAO> GetTravelPlanByIdAsync(Guid travelPlanId)
        {
            var data = await _db.TravelPlans.Include(x=>x.TravelDays).FirstOrDefaultAsync(x => x.Id == travelPlanId);
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TravelPlace, TravelPlaceDAO>();
                cfg.CreateMap<TravelDay, TravelDayDAO>();
                cfg.CreateMap<TravelPlan, TravelPlanDAO>();
            });
            var mapper = config.CreateMapper();
            var result = mapper.Map<TravelPlanDAO>(data);
            return result;
        }

        public async Task<List<TravelPlanDAO>> GetTravelPlansAsync()
        {
            var datas = await _db.TravelPlans.ToListAsync();
            return Mapper.Map<TravelPlan, TravelPlanDAO>(datas,x=> x.ForMember(y=>y.TravelDays,z=>z.Ignore())).ToList();
        }

        public async Task<List<TravelPlaceDAO>> GetTravelPlacesByTravelDayId(Guid travelDayId)
        {
            var travelEntities = await _db.TravelPlaces
                                        .Where(x => x.TravelDayId == travelDayId)
                                        .OrderBy(x => x.TravelTime)
                                        .ToListAsync();

            var travelPlaceDaos = Mapper.Map<TravelPlace, TravelPlaceDAO>(travelEntities).ToList();
            return travelPlaceDaos;
        }

        public async Task<List<TravelPlaceDAO>> UpdateTrafficAndRecalculateTimeAsync(Guid travelDayId, List<TravelPlaceDAO> updatedTravelPlaces) 
        {
            if (updatedTravelPlaces == null || !updatedTravelPlaces.Any())
                return new List<TravelPlaceDAO>();

            var entities = await _db.TravelPlaces
                                    .Where(x => x.TravelDayId == travelDayId)
                                    .OrderBy(x => x.TravelTime)
                                    .ToListAsync();
            if (!entities.Any())
                return new List<TravelPlaceDAO>();

            var inputDict = updatedTravelPlaces.ToDictionary(x => x.Id);

            foreach (var entity in entities)
            {
                if (inputDict.TryGetValue(entity.Id, out var dao))
                {
                    entity.TrafficDuration = dao.TrafficDuration;
                    entity.TrafficType = (int)dao.TrafficType;
                }
            }
            entities = entities.Aggregate(new List<TravelPlace>(), (travels, travel) =>
            {
                if (travels.Count == 0)
                {
                    return new List<TravelPlace>() { travel };
                }
                var lastTravel = travels.Last();
                travel.LastPlaceLeavingTime = lastTravel.TravelTime.AddMinutes(lastTravel.Duration);
                if (!travel.HasArrivedTime)
                {
                    travel.TravelTime = travel.LastPlaceLeavingTime.Value.AddMinutes(travel.TrafficDuration.Value);
                }
                travels.Add(travel);
                return travels;
            });
            await _db.SaveChangesAsync();
            return Mapper.Map<TravelPlace, TravelPlaceDAO>(entities).ToList();
        }

        public async Task UpdateTravelPlaceTimeByIdAsync(TravelPlaceDAO travelPlace)
        {
            var planEntity = await _db.TravelPlaces.FirstOrDefaultAsync(x => x.Id == travelPlace.Id);
            planEntity.TravelTime = travelPlace.TravelTime;
            planEntity.HasArrivedTime = travelPlace.HasArrivedTime;
            planEntity.Duration = travelPlace.Duration;
            await _db.SaveChangesAsync();
        }

        public async Task UpdateTravelTrafficWayByIdAsync(Guid travelPlaceId, TrafficMode trafficMode)
        {
            var entity = await _db.TravelPlaces.FindAsync(travelPlaceId);
            entity.TrafficType = (int)trafficMode;
            await _db.SaveChangesAsync();
        }
    }
}
