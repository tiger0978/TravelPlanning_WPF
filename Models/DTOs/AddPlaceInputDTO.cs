using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanning.Models.DTOs
{
    public class AddPlaceInputDTO
    {
        public Guid MapLayerId { get; set; }
        public string PlaceId { get; set; }
        public string PlaceName { get; set; }
    }
}
