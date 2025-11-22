using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Models.Entities;
using TravelPlanning.Respositories.Models.DAOs;

namespace TravelPlanning.Respositories
{
    public interface ITravelRepository
    {
        Task<List<TravelPlanDAO>> GetTravelPlansAsync();
        Task<TravelPlanDAO> GetTravelPlanByIdAsync(Guid travelPlanId);
        Task<List<TravelPlaceDAO>> GetTravelPlacesAsync(Guid travelPlanId);
        Task<TravelPlanDAO> AddTravelPlanAsync(TravelPlanDAO travelPlan);  
    

    }
}
