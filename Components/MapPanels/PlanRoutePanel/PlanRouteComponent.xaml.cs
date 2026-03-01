using CommunityToolkit.Mvvm.Messaging;
using GoogleMap.SDK.Contract;
using GoogleMap.SDK.Contracts.Commons.Models;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using GoogleMap.SDK.UI.WPF.Components.AutoComplete.Views;
using IoC_Container;
using IoC_Container.Attributes;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TravelPlanning.Attributes;
using TravelPlanning.Messages;
using Wpf.Ui.Controls;
using static GoogleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;


namespace TravelPlanning.Components.MapPanels.PlanRoutePanel
{
    [Transient]
    [NavigationItem("路線規劃", SymbolRegular.Directions24, 2)]
    /// <summary>
    /// PlanRouteComponent.xaml 的互動邏輯
    /// </summary>
    public partial class PlanRouteComponent : UserControl
    {
        private readonly IComponentFactory _componentFactory;

        private readonly IGMap _gmap;
        private readonly PlanRoutePanelContext _context;

        public PlanRouteComponent(IComponentFactory componentFactory, IGMap gmap, PlanRoutePanelContext context)
        {
            InitializeComponent();
            _componentFactory = componentFactory;
            _context = context;
            DataContext = context;
            _gmap = gmap;
            var startPlaceAutoComplete = CreateAutoComplete("Start");
            var endPlaceAutoComplete = CreateAutoComplete("End");
            StartSearchContainer.Children.Add(startPlaceAutoComplete);
            EndSearchContainer.Children.Add(endPlaceAutoComplete);
        }

        private Control CreateAutoComplete(string name)
        {
            var iAutoComplete = _componentFactory.Create<IAutoCompleteView>(typeof(PlaceAutoCompleteView));
            iAutoComplete.SwitchMode();
            PlaceAutoCompleteView view = (PlaceAutoCompleteView)iAutoComplete;
            view.SelectedItem += OnReceivedPlaceDetails;
            var placeAutoComplete = (Control)iAutoComplete;
            placeAutoComplete.Tag = name;
            placeAutoComplete.FontSize = 16;
            placeAutoComplete.VerticalAlignment = VerticalAlignment.Center;
            placeAutoComplete.Background = Brushes.Transparent;
            placeAutoComplete.Padding = new Thickness(5, 0, 0, 0);
            placeAutoComplete.BorderThickness = new Thickness(0);
            return placeAutoComplete;
        }

        private void OnReceivedPlaceDetails(object sender, PlaceDetailResponse e)
        {
            var control = sender as Control;
            if (control.Tag.ToString() == "Start")
            {
                if (_context.StartPlace != null)
                {
                    _gmap.RemoveMarkerElement(new List<Location>()
                    {
                        new Location(
                            _context.StartPlace.result.geometry.location.lat,
                            _context.StartPlace.result.geometry.location.lng
                        )
                    });
                }
                _context.StartPlace = e; 
            }
            else if (control.Tag.ToString() == "End") 
            {
                if (_context.EndPlace != null)
                {
                    _gmap.RemoveMarkerElement(new List<Location>()
                    {
                        new Location(
                            _context.EndPlace.result.geometry.location.lat,
                            _context.EndPlace.result.geometry.location.lng
                        )
                    });
                }
                _context.EndPlace = e;
            }
            _gmap.ClearRoutes();
            WeakReferenceMessenger.Default.Send(new PlaceSelectedMessage(e));
        }
    }
}
