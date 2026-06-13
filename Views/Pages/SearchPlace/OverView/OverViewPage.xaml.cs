using IoC_Container.Attributes;
using System.Windows.Controls;

namespace TravelPlanning.Views.Pages.SearchPlace.OverView
{
    [Transient]
    /// <summary>
    /// OverViewPage.xaml 的互動邏輯
    /// </summary>
    public partial class OverViewPage
    {
        public OverViewPage(OverViewPageContext overViewPageContext)
        {
            InitializeComponent();
            DataContext = overViewPageContext;
        }
    }
}
