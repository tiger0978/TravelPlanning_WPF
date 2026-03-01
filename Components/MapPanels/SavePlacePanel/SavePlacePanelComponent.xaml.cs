using IoC_Container.Attributes;
using System.Windows.Controls;
using TravelPlanning.Attributes;
using Wpf.Ui.Controls;

namespace TravelPlanning.Components.MapPanels.SavePlacePanel
{
    [Transient]
    [NavigationItem("清單", SymbolRegular.Bookmark24, 1)]
    /// <summary>
    /// SavePlacePanelComponent.xaml 的互動邏輯
    /// </summary>
    public partial class SavePlacePanelComponent : UserControl
    {
        public SavePlacePanelContext Context { get; set; }

        public SavePlacePanelComponent(SavePlacePanelContext context)
        {
            InitializeComponent();
            Context = context;
            DataContext = context;
        }
    }
}



















