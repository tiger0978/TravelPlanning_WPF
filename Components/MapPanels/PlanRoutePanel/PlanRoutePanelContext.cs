using CommunityToolkit.Mvvm.Input;
using GoogleMap.SDK.Contract;
using GoogleMap.SDK.Contracts.Commons.Enums;
using GoogleMap.SDK.Contracts.Commons.Models;
using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using IoC_Container.Attributes;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelPlanning.Components.MapPanels.PlanRoutePanel.Models;
using Wpf.Ui.Controls;


namespace TravelPlanning.Components.MapPanels.PlanRoutePanel
{
    [Transient]
    public class PlanRoutePanelContext
    {
        public ICommand SearchRouteCommand { get; set; }
        public ICommand SelectModeCommand { get; set; }

        public PlaceDetailResponse StartPlace;
        public PlaceDetailResponse EndPlace;
        private readonly IGMap _gmap;
        private readonly IGoogleAPIContext _googleAPIContext;
        public ObservableCollection<RouteViewModel> Routes { get; set; } = new ObservableCollection<RouteViewModel>();
        public ObservableCollection<TrafficViewModel> Traffics { get; set; } = new ObservableCollection<TrafficViewModel>()
        {
            new TrafficViewModel(TrafficMode.DRIVE, SymbolRegular.VehicleCar24, true),
            new TrafficViewModel(TrafficMode.TWO_WHEELER, SymbolRegular.VehicleMotorcycle24, false),
            new TrafficViewModel(TrafficMode.BICYCLE, SymbolRegular.VehicleBicycle16, false),
            new TrafficViewModel(TrafficMode.WALK, SymbolRegular.PersonWalking24, false),
            new TrafficViewModel(TrafficMode.TRANSIT, SymbolRegular.VehicleSubway24, false),
        };

        public PlanRoutePanelContext(IGMap gmap, IGoogleAPIContext googleAPIContext) 
        {

            _gmap = gmap;
            _googleAPIContext = googleAPIContext;

            SearchRouteCommand = new RelayCommand(async() =>
            {
                if (StartPlace == null || EndPlace == null)
                {
                    return;
                }  
                await CreateRoute();
            });

            SelectModeCommand = new RelayCommand<TrafficViewModel>(async (selectedItem) =>
            {
                foreach (var item in Traffics) 
                {
                    item.IsSelected = false;
                    selectedItem.IsSelected = true;
                }
                if (StartPlace == null || EndPlace == null)
                {
                    return;
                }
                await CreateRoute();
            });
        }

        private async Task CreateRoute() 
        {
            Routes.Clear();
            _gmap.ClearRoutes();

            var traffic = Traffics.FirstOrDefault(x => x.IsSelected == true);
            var start = new Location(StartPlace.result.geometry.location.lat, StartPlace.result.geometry.location.lng);
            var end = new Location(EndPlace.result.geometry.location.lat, EndPlace.result.geometry.location.lng);
            var result = await _googleAPIContext.Direction.GetDirectionAsync(start,end, traffic.Mode, new List<Avoid> { });

            var routes = result.routes.Select(x => x.polyline.encodedPolyline.ToList()).ToList();
            _gmap.CreateRoute(routes);


            int index = 0;

            foreach (var route in result.routes)
            {
                var routeViewModel = new RouteViewModel(
                    route.description,
                    route.localizedValues.duration.text,
                    route.localizedValues.distance.text,
                    traffic.IconKey, index, _gmap);
                Routes.Add(routeViewModel);

                routeViewModel.RouteDetails = new ObservableCollection<RouteDetailViewModel>(route.legs[0].steps.Select(x =>
                {
                    string instructionText = x.navigationInstruction?.instructions ?? "";
                    var iconKey = SymbolRegular.ArrowUp48;

                    if (!string.IsNullOrEmpty(instructionText))
                    {
                        if (instructionText.Contains("左轉") || instructionText.Contains("左後方轉彎"))
                        {
                            iconKey = SymbolRegular.ArrowTurnUpLeft48;
                        }
                        else if (instructionText.Contains("右轉") || instructionText.Contains("右後方轉彎"))
                        {
                            iconKey = SymbolRegular.ArrowTurnRight48;
                        }
                    }
                    return new RouteDetailViewModel(
                            x.navigationInstruction?.instructions,
                            x.localizedValues.staticDuration?.text,
                            x.localizedValues.distance?.text,
                            iconKey);
                }));
                index++;
            }
        }
    }
}
