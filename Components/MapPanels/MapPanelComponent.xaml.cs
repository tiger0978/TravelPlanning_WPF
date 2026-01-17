using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using GoogleMap.SDK.UI.WPF.Components.AutoComplete.Views;
using IoC_Container;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using TravelPlanning.Models;
using Wpf.Ui.Controls;
using static GoogleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;

namespace TravelPlanning.Components.MapPanels
{
    /// <summary>
    /// MapPanel.xaml 的互動邏輯
    /// </summary>
    public partial class MapPanelComponent : UserControl
    {
        private bool _collapsed = true;
        public MapPanelContext Context;
        public delegate void OnReceivedPlaceDetail(PlaceDetailResponse placeDetailResponse);
        public event OnReceivedPlaceDetail OnSelectedPlace;

        //private SearchPanelContext _searchPanelViewModel = new SearchPanelContext();
        //private SavePlacePanelContext _savePlacePanelViewModel = new SavePlacePanelContext();

        public MapPanelComponent(IComponentFactory componentFactory,IGoogleAPIContext apiContext)
        {
            InitializeComponent();
            Context = new MapPanelContext(apiContext,componentFactory);
            DataContext = Context;




            var iAutoComplete = componentFactory.Create<IAutoCompleteView>(typeof(PlaceAutoCompleteView));
            iAutoComplete.SwitchMode();
            var placeAutoComplete = (Control)iAutoComplete;
            placeAutoComplete.FontSize = 16;
            placeAutoComplete.VerticalAlignment = VerticalAlignment.Center;
            placeAutoComplete.Background = Brushes.Transparent;
            placeAutoComplete.Padding = new Thickness(5, 0, 0, 0);
            placeAutoComplete.BorderThickness = new Thickness(0);
            PlaceAutoCompleteView view = (PlaceAutoCompleteView)iAutoComplete;


            view.SelectedItem += OnReceivedPlaceDetails;
            PlaceContainer.Children.Add(placeAutoComplete);
            Grid.SetRow(placeAutoComplete, 0);
            Grid.SetColumn(placeAutoComplete, 0);


        }

        private void ToggleBtn_Click(object sender, RoutedEventArgs e)
        {
            var animation = new GridLengthAnimation
            {
                Duration = TimeSpan.FromMilliseconds(250),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut },
                From = _collapsed
                    ? new GridLength(0)
                    : new GridLength(440),
                To = _collapsed
                    ? new GridLength(440)
                    : new GridLength(0)
            };

            LeftColumn.BeginAnimation(ColumnDefinition.WidthProperty, animation);
            ToggleIcon.Symbol = _collapsed
                ? SymbolRegular.ChevronLeft12
                : SymbolRegular.ChevronRight12;
            _collapsed = !_collapsed;
        }

        private void OnReceivedPlaceDetails(object sender, PlaceDetailResponse placeDetailResponse) 
        {
            OnSelectedPlace?.Invoke(placeDetailResponse);
            Context.RenderPlaceDetail(placeDetailResponse);
        }
    }
}
