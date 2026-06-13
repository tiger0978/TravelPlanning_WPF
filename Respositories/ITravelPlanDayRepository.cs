using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Respositories.Models.DAOs;

namespace TravelPlanning.Respositories
{
    public interface ITravelPlanDayRepository
    {
        Task AddTravelDayAsync(List<TravelDayDAO> travelDays);
        Task<TravelDayDAO> AddNewTravelDay(Guid travelPlanId);
    }
}
