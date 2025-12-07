using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.Respositories.Models.DAOs;

namespace TravelPlanning.EventHandlers
{
    internal class TravelCardHandler
    {
        public static event EventHandler<TravelPlanDTO> ReceivedTravelCard;

        public static void RenderTravelCard(TravelPlanDTO travelPlanDAO)
        {
            ReceivedTravelCard?.Invoke(null,travelPlanDAO);  
        }
    }
}
