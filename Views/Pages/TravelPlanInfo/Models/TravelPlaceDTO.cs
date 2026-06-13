using GoogleMap.SDK.Contracts.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TravelPlanning.Views.Pages.TravelPlanInfo.Models
{
    public class TravelPlaceDTO
    {
        public Guid Id { get; set; }
        public Guid TravelDayId { get; set; }
        public BitmapImage Cover { get; set; }
        public string PlaceId { get; set; }
        public string Name { get; set; }
        public DateTime TravelTime { get; set; }
        public int Duration { get; set; }
        public bool HasArrivedTime { get; set; }
        public int TrafficDuration { get; set; }
        public TrafficMode TrafficType { get; set; }
        public DateTime? LastPlaceLeavingTime { get; set; }
    }
}
