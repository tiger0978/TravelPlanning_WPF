using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using IoC_Container;
using IoC_Container.Attributes;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using TravelPlanning.Components.MapPanels.SearchPanel;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.Models;
using TravelPlanning.Utilties;

namespace TravelPlanning.Components.MapPanels
{
    [AddINotifyPropertyChangedInterface]
    [Transient]
    public class MapPanelContext
    {
        public UserControl CurrentComponent { get; set; }
        public PlaceDetailResponse SelectedPlaceDetail { get; set; }
        public ICommand SaveMapPlaceCommand { get; set; }
        public ICommand ChangePageCommand { get; set; }
        public List<NavigationPageItem> PageItems { get; set; }

        public Dictionary<Type, UserControl> Pages = new Dictionary<Type, UserControl>();


        public MapPanelContext(IGoogleAPIContext apiContext, IComponentFactory componentFactory, 
            NavigationProvider navigationProvider)
        {
            PageItems = NavigationPageProvider.GetPages<NavigationPageItem>("TravelPlanning.Components.MapPanels");
            ChangePageCommand = new RelayCommand<Type>(pageType =>
            {
                navigationProvider.Navigate(pageType, null);
            });

            SaveMapPlaceCommand = new RelayCommand(() =>
            {
                var mapPlaceDTO = new FavoriteTravelDTO()
                {
                    PlaceId = SelectedPlaceDetail.result.place_id
                };
            });
        }
 
        public void RenderPlaceDetail(PlaceDetailResponse placeDetailResponse)
        {
            if(CurrentComponent.DataContext is SearchPanelContext searchPanelContext){

                searchPanelContext.RenderModel(placeDetailResponse);
            }
        }
    }
}
