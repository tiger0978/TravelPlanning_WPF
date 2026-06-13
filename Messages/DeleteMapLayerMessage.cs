using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanning.Messages
{
    public class DeleteMapLayerMessage
    {
        public Guid MapLayerId { get; set; }
        public DeleteMapLayerMessage(Guid maplayerId)
        {
            MapLayerId = maplayerId;
        }
    }
}
