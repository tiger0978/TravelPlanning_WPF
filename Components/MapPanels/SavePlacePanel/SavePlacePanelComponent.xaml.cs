using GoogleMap.SDK.Contracts.GoogleAPI;
using IoC_Container;
using System.Windows.Controls;
using TravelPlanning.Attributes;
using Wpf.Ui.Controls;

namespace TravelPlanning.Components.MapPanels.SavePlacePanel
{
    [NavigationItem("清單", SymbolRegular.Bookmark24, 1)]
    /// <summary>
    /// SavePlacePanelComponent.xaml 的互動邏輯
    /// </summary>
    public partial class SavePlacePanelComponent : UserControl
    {
        public SavePlacePanelContext Context { get; set; }

        public SavePlacePanelComponent(IGoogleAPIContext apiContext, IComponentFactory componentFactory)
        {
            InitializeComponent();
            var context = new SavePlacePanelContext(componentFactory);
            Context = context;
            DataContext = context;
        }
    }
}
