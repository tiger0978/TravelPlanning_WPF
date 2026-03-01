using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanning.Respositories.Models.DAOs
{
    public class MapPlaceDAO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MapLayerId { get; set; }
        public string PlaceId { get; set; }
        public string Name { get; set; }

        public MapPlaceDAO() { }

        public MapPlaceDAO(Guid mapLayerId, string placeId, string name)
        {
            MapLayerId = mapLayerId;
            PlaceId = placeId;
            Name = name;
        }
    }
}
