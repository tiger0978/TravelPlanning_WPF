using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using IoC_Container;
using IoC_Container.Attributes;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelPlanning.Utilties;
using TravelPlanning.Utilties.Navigation;

namespace TravelPlanning.Views.Pages.SearchPlace.OverView
{
    [AddINotifyPropertyChangedInterface]
    [Transient]
    public class OverViewPageContext : INavigationAware
    {
        public string Addresses { get; set; }
        public string Phone { get; set; }
        public string OpeningTime { get; set; }
        public string WebSite { get; set; }
        public bool ShowPopup { get; set; } = false;


        public OverViewPageContext(IPresenterFactory presenterFactory,
            IComponentFactory componentFactory, NavigationProvider navigationProvider)
        {
        }

        public void SendAware(object parm)
        {
            var response = (PlaceDetailResponse)parm;

            Addresses = response.result.formatted_address;
            WebSite = response.result.website;
            Phone = response.result.formatted_phone_number;
            string[] openingHours = response.result.current_opening_hours?
                        .weekday_text ?? new string[0];

            OpeningTime = string.Join("\n", openingHours);

        }
    }
}
