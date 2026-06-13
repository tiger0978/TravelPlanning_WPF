using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanning.Views.Pages.TravelPlanInfo.Models
{
    public class TravelPlanDayDTO
    {
        public Guid Id { get; set; }
        public DateTime TravelDate { get; set; }
        public string DayName { get; set; }
        public List<TravelPlaceDTO> TravelPlaces  { get; set; }
    }
}
