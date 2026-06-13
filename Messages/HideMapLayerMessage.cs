using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanning.Messages
{
    internal class HideMapLayerMessage
    {
        public bool IsHidden { get; set; }
        public Guid MapLayerId { get; set; }

        public HideMapLayerMessage(Guid mapLayerId, bool isHidden) 
        {
            MapLayerId = mapLayerId;
            IsHidden = isHidden;
        }
    }
}
