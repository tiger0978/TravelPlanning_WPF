using CommunityToolkit.Mvvm.Messaging;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using TravelPlanning.Components.MapPanels.SearchPanel;
using TravelPlanning.Messages;
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
        public MapPanelContext Context;

        public MapPanelComponent()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Context = (MapPanelContext)DataContext;
            Context.NavigationProvider.SetControl(this.ContentContainer);
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            AnimatePanel(false);
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            AnimatePanel(true);
        }
        private void AnimatePanel(bool expand)
        {
            var animation = new GridLengthAnimation
            {
                Duration = TimeSpan.FromMilliseconds(250),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut },
                From = expand ? new GridLength(0) : new GridLength(440),
                To = expand ? new GridLength(440) : new GridLength(0)
            };
            LeftColumn.BeginAnimation(ColumnDefinition.WidthProperty, animation);
        }

    }
}
