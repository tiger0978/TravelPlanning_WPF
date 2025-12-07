using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelPlanning.Attributes;

namespace TravelPlanning.Views.Pages.PlaceSearch
{
    /// <summary>
    /// PlaceSearch.xaml 的互動邏輯
    /// </summary>
    [NavigationItem("景點搜尋", Wpf.Ui.Controls.SymbolRegular.HeartCircle24, 2)]

    public partial class PlaceSearch : Page
    {
        public PlaceSearch()
        {
            InitializeComponent();
        }
    }
}
