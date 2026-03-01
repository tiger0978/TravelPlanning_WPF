using CommunityToolkit.Mvvm.Messaging;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using GoogleMap.SDK.UI.WPF.Components.AutoComplete.Views;
using IoC_Container;
using IoC_Container.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using TravelPlanning.Messages;
using Wpf.Ui.Controls;
using static GoogleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;
using static TravelPlanning.Components.MapPanels.MapPanelComponent;

namespace TravelPlanning.Components.MapPanels.AddSavePlaceList
{
    [Transient]
    [NavigationItem("路線規劃", SymbolRegular.Directions24, 3)]
    /// <summary>
    /// AddSaveListComponent.xaml 的互動邏輯
    /// </summary>
    public partial class AddSaveListComponent : UserControl
    {
        public AddSaveListComponent(IComponentFactory componentFactory)
        {
            InitializeComponent();
            var iAutoComplete = componentFactory.Create<IAutoCompleteView>(typeof(PlaceAutoCompleteView));
            iAutoComplete.SwitchMode();
            var placeAutoComplete = (Control)iAutoComplete;
            placeAutoComplete.FontSize = 16;
            placeAutoComplete.VerticalAlignment = VerticalAlignment.Center;
            placeAutoComplete.Background = Brushes.Transparent;
            placeAutoComplete.Padding = new Thickness(5, 0, 0, 0);
            placeAutoComplete.BorderThickness = new Thickness(0);
            PlaceAutoCompleteView view = (PlaceAutoCompleteView)iAutoComplete;
            view.SelectedItem += OnReceivedPlaceDetails;
            PlaceContainer.Children.Add(placeAutoComplete);
            Grid.SetRow(placeAutoComplete, 0);
            Grid.SetColumn(placeAutoComplete, 0);
            DataContext = new AddSaveListContext();
        }
        private void OnReceivedPlaceDetails(object sender, PlaceDetailResponse e)
        {
            WeakReferenceMessenger.Default.Send(new PlaceSelectedMessage(e));
        }
    }
}
