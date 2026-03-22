using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanning.Respositories.Models.DAOs
{
    public class MapLayerDAO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string IconKey { get; set; }
        public List<MapPlaceDAO> MapPlaces { get; set; }

        public MapLayerDAO() { }
        public MapLayerDAO(string name)
        {
            Name = name;
        }
    }
}
