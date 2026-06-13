using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using IoC_Container;
using IoC_Container.Attributes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using TravelPlanning.Utilties;
using TravelPlanning.Views.Pages.SearchPlace.OverView;

namespace TravelPlanning.Components.MapPanels.SearchPanel
{
    [Transient]
    //[NavigationItem("搜尋", SymbolRegular.SearchSquare24, 0)]
    /// <summary>
    /// SearchPanel.xaml 的互動邏輯
    /// </summary>
    public partial class SearchPanelComponent : UserControl
    {
        public SearchPanelContext Context { get; set; }
        public SearchPanelComponent(SearchPanelContext context, IComponentFactory componentFactory)
        {
            InitializeComponent();
            Context = context;
            Context.NavigationProvider.SetFrame(this.Frame);
            DataContext = context;
        }

        private void TabOverview_Checked(object sender, RoutedEventArgs e)
        {
            if (OverviewPanel == null) return;
            AnimateIndicator(0);
        }
        private void TabComment_Checked(object sender, RoutedEventArgs e)
        {
            if (OverviewPanel == null) return;

            AnimateIndicator(SlidingIndicator.ActualWidth);
        }

        private void AnimateIndicator(double toX)
        {
            var anim = new DoubleAnimation
            {
                To = toX,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            IndicatorTransform.BeginAnimation(TranslateTransform.XProperty, anim);
        }
    }
}
