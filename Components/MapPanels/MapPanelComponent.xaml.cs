using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using IoC_Container;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using TravelPlanning.Models;
using TravelPlanning.Utilties;
using Wpf.Ui.Controls;

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


        public MapPanelComponent(IComponentFactory componentFactory, MapPanelContext mapPanelContxt, NavigationProvider navigationProvider)
        {
            InitializeComponent();
            Context = mapPanelContxt;
            DataContext = Context;
            navigationProvider.SetControl(this.ContentContainer);
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
    }
}
