using CommunityToolkit.Mvvm.Messaging;
using GoogleMap.SDK.Contract;
using GoogleMap.SDK.Contracts.Commons.Enums;
using GoogleMap.SDK.Contracts.Commons.Models;
using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using GoogleMap.SDK.UI.WPF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TravelPlanning.Messages.TravelPlanInfo;
using TravelPlanning.Views.Pages.TravelPlanInfo.Models;

namespace TravelPlanning.Views.Pages.TravelPlanInfo
{
    /// <summary>
    /// TravelPlanInfoPage.xaml 的互動邏輯
    /// </summary>
    public partial class TravelPlanInfoPage : Page
    {
        private readonly IGMap _gmap;
        private readonly IGoogleAPIContext _googleAPIContext;
        public TravelPlanInfoPage(IGMap gmap, IGoogleAPIContext googleAPIContext)
        {
            InitializeComponent();
            _gmap = gmap;
            _googleAPIContext = googleAPIContext;
            Unloaded += MyPage_Unloaded;

            WeakReferenceMessenger.Default.Register<RouteRenderMessage>(this, async (r, m) =>
            {
                await TravelList_OnTravelPlaceChanged(m.Value);
            });
        }

        private void MyPage_Unloaded(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.UnregisterAll(this);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (MapContainer.Children.Count == 0)
            {
                var map = _gmap as UserControl;
                MapContainer.Children.Add(map);
            }
            ((TravelPlanInfoContext)DataContext).ScrollRequested += delta =>
            {
                TabScrollViewer.ScrollToHorizontalOffset(
                    TabScrollViewer.HorizontalOffset + delta);
            };
        }

        private async Task TravelList_OnTravelPlaceChanged(List<TravelPlaceDTO> places)
        {
            _gmap.ClearOverlay();
            if(places.Count == 0) 
            {
                return;
            }
            if(places.Count == 1) 
            {
                var placeResponse = await _googleAPIContext.Place.PlaceDetailAsync(places[0].PlaceId);
                CreateMarker(placeResponse, places[0].TravelDayId.ToString());
            }
            else if (places.Count == 2)
            { 
                var routeResponse = await _googleAPIContext.Direction.GetDirectionAsync(places[0].PlaceId, places[1].PlaceId, TrafficMode.DRIVE, new List<Avoid> { });
                var routes = routeResponse.routes.Select(x => x.polyline.encodedPolyline.ToList()).ToList();
                _gmap.CreateRoute(routes, places[0].TravelDayId.ToString());
                var startLocation = new Location
                {
                    latLng = routeResponse.routes[0].legs[0].startLocation.latLng
                };
                var endLocation = new Location
                {
                    latLng = routeResponse.routes[0].legs[0].endLocation.latLng
                };
                _gmap.CreateMarker(startLocation, places[0].TravelDayId.ToString());
                _gmap.CreateMarker(endLocation, places[0].TravelDayId.ToString());

            }
            else 
            {
                var startPlaceId = places.First().PlaceId;
                var wayPoints = places.Skip(1).Take(places.Count - 1).Select(x => x.PlaceId).ToList();
                var routeResponse = await _googleAPIContext.Direction.GetDirectionAsync(places[0].PlaceId, places.Last().PlaceId, TrafficMode.DRIVE, new List<Avoid> { }, wayPoints);

                var locations = routeResponse.routes[0].legs.Select(x => new Location
                {
                    latLng = x.startLocation.latLng
                }).ToList();
                var endLocation = new Location { latLng = routeResponse.routes[0].legs.Last().endLocation.latLng };
                locations.Add(endLocation);


                foreach(var location in locations) 
                {
                    _gmap.CreateMarker(location, places[0].TravelDayId.ToString());
                }

                var routes = routeResponse.routes.Select(x => x.polyline.encodedPolyline.ToList()).ToList();
                _gmap.CreateRoute(routes, places[0].TravelDayId.ToString());
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
        private void SpotMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;

            // 確保 ContextMenu 存在，避免 NullReferenceException
            if (btn.ContextMenu != null)
            {
                // 🌟 關鍵：手動告訴 ContextMenu 它的「目標位置」是這顆按鈕
                // 這樣你在 XAML 裡寫的 PlacementTarget 才會有東西！
                btn.ContextMenu.PlacementTarget = btn;

                // 打開選單
                btn.ContextMenu.IsOpen = true;
            }
        }
    }
}
