using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanning.Messages
{
    public class SaveListPlacesLoadedMessage
    {
        public List<string> PlaceIds { get; set; }
        public Guid MapLayerId { get; set; }

        public SaveListPlacesLoadedMessage(List<string> placeIds, Guid mapLayerId)
        {
            PlaceIds = placeIds;
            MapLayerId = mapLayerId;
        }
    }
}
