using GoogleMap.SDK.Contract;
using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.UI.WPF.Components.AutoComplete.Views;
using IoC_Container;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static GoogleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;

namespace TravelPlanning.Views.Pages.FavoriteTravel
{
    /// <summary>
    /// FavoriteTravel.xaml 的互動邏輯
    /// </summary>
    [NavigationItem("我的最愛", Wpf.Ui.Controls.SymbolRegular.HeartCircle24, 3)]
    public partial class FavoriteTravel : Page
    {
        private IGMap _gmap;
        public FavoriteTravel(IComponentFactory componentFactory, IGMap gmap, IGoogleAPIContext context)
        {
            InitializeComponent();
            var iAutoComplete = componentFactory.Create<IAutoCompleteView>(typeof(PlaceAutoCompleteView));
            iAutoComplete.SwitchMode();
            var placeAutoComplete = (Control)iAutoComplete;
            placeAutoComplete.FontSize = 16;
            placeAutoComplete.VerticalAlignment = VerticalAlignment.Center;
            placeAutoComplete.Background = Brushes.Transparent;
            placeAutoComplete.Padding= new Thickness(5,0,0,0);
            placeAutoComplete.BorderThickness = new Thickness(0);
            _gmap = gmap;
            var map = (UserControl)_gmap;
            Button button = new Button();
            button.Content = "HELLO";
            MapContainer.Children.Add(button);
            PlaceContainer.Children.Add(placeAutoComplete);
            Grid.SetRow(placeAutoComplete, 0);  
            Grid.SetColumn(placeAutoComplete, 0);
        }
    }
}
