using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TravelPlanning.Views.Pages.TravelPlanInfo.Models
{
    public class AddTravelPlaceDTO
    {
        public Guid TravelDayId { get; set; }
        public string PlaceId { get; set; }
        public string Name { get; set; }
        public DateTime TravelTime { get; set; }
        public int Duration { get; set; }
    }
}
