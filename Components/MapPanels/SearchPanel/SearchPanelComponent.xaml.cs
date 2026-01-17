using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using IoC_Container;
using System.Windows.Controls;
using TravelPlanning.Attributes;
using Wpf.Ui.Controls;

namespace TravelPlanning.Components.MapPanels.SearchPanel
{
    [NavigationItem("搜尋", SymbolRegular.SearchSquare24, 1)]
    /// <summary>
    /// SearchPanel.xaml 的互動邏輯
    /// </summary>
    public partial class SearchPanelComponent : UserControl
    {
        public delegate void SearchPlace(PlaceDetailResponse response);
        public event SearchPlace OnReceivedPlace;
        public SearchPanelContext Context { get; set; }

        public SearchPanelComponent(IGoogleAPIContext apiContext, IComponentFactory componentFactory)
        {
            InitializeComponent();

            var context = new SearchPanelContext(apiContext,componentFactory);
            Context = context;
            DataContext = context;
        }

        private void OnReceivedPlaceDetail(object sender, PlaceDetailResponse e)
        {
            // OnReceivedPlace.Invoke(e);
            Context.ReceivedPlaceCommand.Execute(e);
        }
    }
}
