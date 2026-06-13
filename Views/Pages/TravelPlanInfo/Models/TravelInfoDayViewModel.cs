using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanning.Views.Pages.TravelPlanInfo.Models
{
    [AddINotifyPropertyChangedInterface]
    public class TravelInfoDayViewModel
    {
        public Guid Id { get; set; }
        public string DayName { get; set; }
        public string TravelDate { get; set; }
        public List<TravelInfoPlaceViewModel> TravelPlaces { get; set; }

    }
}
