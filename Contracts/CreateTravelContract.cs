using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Contracts.DTOs;

namespace TravelPlanning.Contracts
{
    public interface ICreateTravelView 
    {

    }
    public interface ICreateTravelPresenter 
    {
        Task AddTravelPlan(TravelPlanDTO travelPlan);
    }
}
