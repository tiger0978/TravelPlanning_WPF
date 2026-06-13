using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanning.Views.Pages.TravelPlanInfo.Models
{
    public class DeleteTravelPlaceDTO
    {
        public Guid TravelDayId { get; set; }
        public Guid TravelPlaceId { get; set; }
    }
}
