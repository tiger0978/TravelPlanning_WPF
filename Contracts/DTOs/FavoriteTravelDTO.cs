using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanning.Contracts.DTOs
{
    public class FavoriteTravelDTO
    {
        public Guid Id { get; set; }
        public Guid MapLayerId { get; set; }
        public string PlaceId { get; set; }
        public string Name { get; set; }
        public string MapLayerName { get; set; }
    }
}
