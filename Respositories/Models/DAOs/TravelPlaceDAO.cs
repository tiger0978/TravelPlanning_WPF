using GoogleMap.SDK.Contracts.Commons.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Models.Entities;

namespace TravelPlanning.Respositories.Models.DAOs
{
    public class TravelPlaceDAO
    {
        public Guid Id { get; set; }
        public Guid TravelDayId { get; set; }
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
