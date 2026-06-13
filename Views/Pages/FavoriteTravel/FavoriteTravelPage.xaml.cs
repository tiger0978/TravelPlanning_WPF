using CommunityToolkit.Mvvm.Messaging;
using GoogleMap.SDK.Contract;
using GoogleMap.SDK.Contract.Components.Gmap.Models;
using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using GoogleMap.SDK.UI.WPF;
using IoC_Container;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using TravelPlanning.Attributes;
using TravelPlanning.Components.MapPanels.AddSavePlaceList;
using TravelPlanning.Components.MapPanels.PlanRoutePanel;
using TravelPlanning.Components.MapPanels.SearchPanel;
using TravelPlanning.Messages;
using TravelPlanning.Models.DTOs;
using TravelPlanning.Utilties;

namespace TravelPlanning.Views.Pages.FavoriteTravel
{
    /// <summary>
    /// FavoriteTravel.xaml 的互動邏輯
    /// </summary>
    [NavigationItem("我的最愛", Wpf.Ui.Controls.SymbolRegular.HeartCircle24, 3)]
    public partial class FavoriteTravelPage : Page
    {
        private IGMap _gmap;
        public FavoriteTravelContext favoriteTravelContext { get; set; }
        private readonly NavigationProvider _navigationProvider;
        private readonly IGoogleAPIContext _googleAPIContext;
        private readonly IComponentFactory _componentFactory;
        public FavoriteTravelPage(IComponentFactory componentFactory, IPresenterFactory presenterFactory,
            NavigationProvider navigationProvider, IGMap gmap, IGoogleAPIContext googleAPIContext, FavoriteTravelContext favoriteTravelContext)
        {
            InitializeComponent();
            _componentFactory = componentFactory;
            _googleAPIContext = googleAPIContext;
            _navigationProvider = navigationProvider;
            this.favoriteTravelContext = favoriteTravelContext;
            _gmap = gmap;

        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = favoriteTravelContext;
            _gmap.OnMarkerClicked += _gmap_OnMarkerClicked;

            if(MapContainer.Children.Count ==0) 
            {
                var map = _gmap as UserControl;
                MapContainer.Children.Add(map);
            }
            await RenderGmapMarkers();
            WeakReferenceMessenger.Default.Register<PlaceSelectedMessage>(this, (r, m) =>
            {
                SearchPanel_OnReceivedPlace(m.Value);
            });
            WeakReferenceMessenger.Default.Register<SaveListPlacesLoadedMessage>(this, (r, m) =>
            {
                AddSaveListPlace_OnLoadPlaces(m);
            });
            WeakReferenceMessenger.Default.Register<InitialMapOverlayMessage>(this, async (r, m) =>
            {
                await RenderGmapMarkers();
            });
            WeakReferenceMessenger.Default.Register<HideMapLayerMessage>(this, (r, m) =>
            {
                HideMapLayer(m);
            });
            WeakReferenceMessenger.Default.Register<DeleteMapLayerMessage>(this, (r, m) =>
            {
                DeleteMaplayer(m);
            });
        }


        private void _gmap_OnMarkerClicked(object sender, MarkerInfo e)
        {
            var data = (PlaceDetailResponse)e.Tag;
            var currentComponent = _navigationProvider.ContentControl.Content;
            if (currentComponent is AddSaveListComponent || currentComponent is PlanRouteComponent) 
            {
                //var test = MapPanel;
                //popup.PlacementTarget = ((MapPanelComponent)MapPanel).ToggleButton;
                popup.Placement = PlacementMode.Right;
                favoriteTravelContext.PopupContent = _componentFactory.Create<SearchPanelComponent>();
                var userControl = favoriteTravelContext.PopupContent;
                var panelContext = ((SearchPanelComponent)userControl).Context;
                panelContext.RenderModel(data);
                //((SearchPanelComponent)favoriteTravelContext.PopupContent).Context.RenderModel(data);
                favoriteTravelContext.IsPopupOpen = true;
            }
            else 
            {
                favoriteTravelContext.MapPanelContext.ToggleButtonVisibility = Visibility.Visible;
                //popup.PlacementTarget = ((MapPanelComponent)MapPanel.Content).ToggleButton;
                //popup.Placement = PlacementMode.Right;
                var userControl = _navigationProvider.Navigate(typeof(SearchPanelComponent), null);
                var panelContext = ((SearchPanelComponent)userControl).Context;
                panelContext.RenderModel(data);
            }
        }

        private async Task RenderGmapMarkers() 
        {
            _gmap.ClearOverlay();
            var mapPlaces = await favoriteTravelContext.GetAllMapPlaces();
            CreateMarkers(mapPlaces);
        }

        private void AddSaveListPlace_OnLoadPlaces(SaveListPlacesLoadedMessage message) 
        {
            _gmap.ClearOverlay();
            var mapPlaces = message.PlaceIds.Select(x => new MapPlaceDTO
            {
                PlaceId = x,
                MapLayerId = message.MapLayerId
            }).ToList();
            CreateMarkers(mapPlaces);
        }

        private void SearchPanel_OnReceivedPlace(PlaceDetailResponse response)
        {
            _gmap.ClearOverlay();
            CreateMarker(response, "MapOverlay");
        }

        private async void HideMapLayer(HideMapLayerMessage message) 
        {
            if (message.IsHidden) 
            {
                _gmap.ShowOverlay(message.MapLayerId.ToString());
            }
            else 
            {
                _gmap.HideOverlay(message.MapLayerId.ToString());
            }
        }
        private async void DeleteMaplayer(DeleteMapLayerMessage message)
        {
           _gmap.ClearOverlay(message.MapLayerId.ToString());
        }

        private async void CreateMarkers(List<MapPlaceDTO> mapPlaces) 
        {
            foreach (var mapPlace in mapPlaces)
            {
                var res = await _googleAPIContext.Place.PlaceDetailAsync(mapPlace.PlaceId);
                CreateMarker(res, mapPlace.MapLayerId.ToString());
            }
        }

        private void CreateMarker(PlaceDetailResponse response, string mapLayerName) 
        {
            MapInfoToolTipData data = new MapInfoToolTipData()
            {
                Title = response.result.name,
                Address = response.result.formatted_address,
            };
            var tooltipStyle = (Style)FindResource("MapInfoToolTipStyle");
            var toolTip = new ToolTip
            {
                Style = tooltipStyle,
                DataContext = data
            };
            _gmap.CreateMarker(response.result.geometry.location.lat, response.result.geometry.location.lng, mapLayerName, toolTip: toolTip, data: response);
        }
    }
}
