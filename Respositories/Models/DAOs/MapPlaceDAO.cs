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
        public Guid Id { get; set; }
        public Guid MapLayerId { get; set; }
        public string PlaceId { get; set; }
        public string Name { get; set; }
    }
}
