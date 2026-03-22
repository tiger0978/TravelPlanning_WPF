using CommunityToolkit.Mvvm.Messaging;
using GoogleMap.SDK.Contract;
using GoogleMap.SDK.Contract.Components.Gmap.Models;
using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using GoogleMap.SDK.UI.WPF;
using IoC_Container;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using TravelPlanning.Attributes;
using TravelPlanning.Components.MapPanels;
using TravelPlanning.Components.MapPanels.AddSavePlaceList;
using TravelPlanning.Components.MapPanels.PlanRoutePanel;
using TravelPlanning.Components.MapPanels.SearchPanel;
using TravelPlanning.Messages;
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
        public FavoriteTravelPage(IComponentFactory componentFactory, IPresenterFactory presenterFactory,
            NavigationProvider navigationProvider, IGMap gmap, IGoogleAPIContext googleAPIContext, FavoriteTravelContext favoriteTravelContext)
        {
            InitializeComponent();
            _googleAPIContext = googleAPIContext;
            _navigationProvider = navigationProvider;
            this.favoriteTravelContext = favoriteTravelContext;
            _gmap = gmap;

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = favoriteTravelContext;
            _gmap.OnMarkerClicked += _gmap_OnMarkerClicked;
            var map = _gmap as UserControl;
            MapContainer.Children.Add(map);
            RenderGmapMarkers();
            WeakReferenceMessenger.Default.Register<PlaceSelectedMessage>(this, (r, m) =>
            {
                SearchPanel_OnReceivedPlace(m.Value);
            });
            WeakReferenceMessenger.Default.Register<SaveListPlacesLoadedMessage>(this, (r, m) =>
            {
                AddSaveListPlace_OnLoadPlaces(m);
            });
            WeakReferenceMessenger.Default.Register<InitialMapOverlayMessage>(this, (r, m) =>
            {
                RenderGmapMarkers();
            });
        }


        private void _gmap_OnMarkerClicked(object sender, MarkerInfo e)
        {
            var data = (PlaceDetailResponse)e.Tag;
            var currentComponent = _navigationProvider.ContentControl.Content;
            if (currentComponent is AddSaveListComponent || currentComponent is PlanRouteComponent) 
            {
                popup.PlacementTarget = ((MapPanelComponent)MapPanel.Content).ToggleButton;
                popup.Placement = PlacementMode.Right;
                favoriteTravelContext.IsPopupOpen = true;
                ((SearchPanelComponent)favoriteTravelContext.PopupContent).Context.RenderModel(data);
            }
            else 
            {
                var userControl = _navigationProvider.Navigate(typeof(SearchPanelComponent), null);
                var panelContext = ((SearchPanelComponent)userControl).Context;
                panelContext.RenderModel(data);
            }
        }

        private async void RenderGmapMarkers() 
        {
            _gmap.ClearOverlay();
            var mapPlaces = await favoriteTravelContext.GetAllMapPlaces();
            foreach (var mapPlace in mapPlaces)
            {
                var res = await _googleAPIContext.Place.PlaceDetailAsync(mapPlace.PlaceId);
                CreateMarker(res, mapPlace.MapLayerId.ToString());
            }
        }

        private async void AddSaveListPlace_OnLoadPlaces(SaveListPlacesLoadedMessage message) 
        {
            _gmap.ClearOverlay();
            foreach (var placeId in message.PlaceIds) 
            {
                var res = await _googleAPIContext.Place.PlaceDetailAsync(placeId);
                CreateMarker(res, message.MapLayerId.ToString());
            }
        }

        private void SearchPanel_OnReceivedPlace(PlaceDetailResponse response)
        {
            _gmap.ClearOverlay();
            CreateMarker(response, "SerachPanel");
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
            _gmap.CreateMarker(response.result.geometry.location.lat, response.result.geometry.location.lng, toolTip: toolTip, data: response);
        }


    }
}
