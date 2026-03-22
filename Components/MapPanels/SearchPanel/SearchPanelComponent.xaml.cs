using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using IoC_Container;
using IoC_Container.Attributes;
using System.Windows.Controls;

namespace TravelPlanning.Components.MapPanels.SearchPanel
{
    [Transient]
    //[NavigationItem("搜尋", SymbolRegular.SearchSquare24, 0)]
    /// <summary>
    /// SearchPanel.xaml 的互動邏輯
    /// </summary>
    public partial class SearchPanelComponent : UserControl
    {
        public delegate void SearchPlace(PlaceDetailResponse response);


        public event SearchPlace OnReceivedPlace;
        public SearchPanelContext Context { get; set; }

        public SearchPanelComponent(SearchPanelContext context, IComponentFactory componentFactory)
        {
            InitializeComponent();
            
            Context = context;
            DataContext = context;
        }

    }
}
