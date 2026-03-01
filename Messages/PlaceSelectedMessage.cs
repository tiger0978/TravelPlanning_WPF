using CommunityToolkit.Mvvm.Messaging.Messages;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;

namespace TravelPlanning.Messages
{
    public class PlaceSelectedMessage : ValueChangedMessage<PlaceDetailResponse>
    {
        public PlaceSelectedMessage(PlaceDetailResponse value) : base(value)
        {
        }
    }
}
