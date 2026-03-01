using CommunityToolkit.Mvvm.Messaging;
using GoogleMap.SDK.Contract;
using GoogleMap.SDK.Contract.Components.Gmap.Models;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using GoogleMap.SDK.UI.WPF;
using IoC_Container;
using System.Windows;
using System.Windows.Controls;
using TravelPlanning.Attributes;
using TravelPlanning.Components.MapPanels;
using TravelPlanning.Components.MapPanels.SearchPanel;
using TravelPlanning.Messages;

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
        private MapPanelComponent _mapPanel { get; set; }


        public FavoriteTravelPage(IComponentFactory componentFactory,IPresenterFactory presenterFactory, IGMap gmap)
        {
            InitializeComponent();
            favoriteTravelContext = new FavoriteTravelContext(presenterFactory);
            DataContext = favoriteTravelContext;
            _mapPanel = componentFactory.Create<MapPanelComponent>();
            PlaceContainer.Children.Add(_mapPanel);
            Grid.SetRow(_mapPanel, 0);
            Grid.SetColumn(_mapPanel, 0);
            _gmap = gmap;
            _gmap.OnMarkerClicked += _gmap_OnMarkerClicked;

            var map = gmap as UserControl;
            MapContainer.Children.Add(map);

            _mapPanel.OnSelectedPlace += SearchPanel_OnReceivedPlace;

            WeakReferenceMessenger.Default.Register<PlaceSelectedMessage>(this, (r, m) =>
            {
                // m.Value 就是傳過來的 PlaceDetailResponse
                SearchPanel_OnReceivedPlace(m.Value);
            });
        }

        private async void _gmap_OnMarkerClicked(object sender, MarkerInfo e)
        {
            var data = (PlaceDetailResponse)e.Tag;
            var panelContext = ((SearchPanelComponent)_mapPanel.Context.Pages[typeof(SearchPanelComponent)]).Context;
            panelContext.RenderModel(data);
        }

        private void SearchPanel_OnReceivedPlace(PlaceDetailResponse response)
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
            _gmap.CreateMarker(response.result.geometry.location.lat, response.result.geometry.location.lng,toolTip: toolTip, data: response );
        }
    }
}
