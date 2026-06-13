using GoogleMap.SDK.Contracts.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelPlanning.Respositories.Models.DAOs;

namespace TravelPlanning.Respositories
{
    public interface ITravelRepository
    {
        Task<List<TravelPlanDAO>> GetTravelPlansAsync();
        Task<TravelPlanDAO> GetTravelPlanByIdAsync(Guid travelPlanId);
        Task<List<TravelPlaceDAO>> GetTravelPlacesAsync(Guid travelPlanId);
        Task<List<TravelPlaceDAO>> UpdateTrafficAndRecalculateTimeAsync(Guid travelDayId, List<TravelPlaceDAO> updatedTravelPlaces);
        Task<TravelPlanDAO> AddTravelPlanAsync(TravelPlanDAO travelPlan); 
        Task<bool> DeleteTravelPlanByIdAsync(Guid id);
        Task<TravelPlaceDAO> AddTravelPlaceAsync(TravelPlaceDAO travelPlace);
        Task DeleteTravelPlaceByIdAsync(Guid placeId);
        Task<List<TravelPlaceDAO>> GetTravelPlacesByTravelDayId(Guid travelDay);
        Task UpdateTravelPlaceTimeByIdAsync(TravelPlaceDAO travelPlace);
        Task UpdateTravelTrafficWayByIdAsync(Guid travelPlaceId, TrafficMode trafficMode);
    }
}
