using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using IoC_Container;
using IoC_Container.Attributes;
using PropertyChanged;
using System;
using System.Collections.Generic;
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


        public MapPanelContext(IGoogleAPIContext apiContext, IComponentFactory componentFactory)
        {
            PageItems = NavigationPageProvider.GetPages<NavigationPageItem>("TravelPlanning.Components.MapPanels");

            CurrentComponent = (UserControl)Activator.CreateInstance(PageItems[0].TargetPageType, apiContext, componentFactory);
           
            

            foreach (var item in PageItems) 
            {
                Pages.Add(item.TargetPageType, (UserControl)Activator.CreateInstance(item.TargetPageType, apiContext, componentFactory));
            }

            ChangePageCommand = new RelayCommand<Type>(pageType =>
            {
                if(!Pages.TryGetValue(pageType, out UserControl userControl))
                {
                    userControl = (UserControl)Activator.CreateInstance(pageType, apiContext, componentFactory);
                    Pages.Add(pageType, userControl);
                }
                CurrentComponent = userControl;
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
