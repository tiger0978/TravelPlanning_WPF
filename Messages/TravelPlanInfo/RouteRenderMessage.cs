using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Collections.Generic;
using TravelPlanning.Views.Pages.TravelPlanInfo.Models;

namespace TravelPlanning.Messages.TravelPlanInfo
{
    public class RouteRenderMessage : ValueChangedMessage<List<TravelPlaceDTO>>
    {
        public RouteRenderMessage(List<TravelPlaceDTO> value) : base(value)
        {
        }
    }
}
