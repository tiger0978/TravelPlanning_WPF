using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using GoogleMap.SDK.UI.WPF.Components.AutoComplete.Views;
using IoC_Container;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static GoogleMap.SDK.Contracts.Components.AutoComplete.Contracts.AutoCompleteContract;

namespace TravelPlanning.Components.AutoComplete
{
    /// <summary>
    /// AutoCompleteComponent.xaml 的互動邏輯
    /// </summary>
    public partial class AutoCompleteComponent : UserControl
    {
        public AutoCompleteComponent()
        {
            InitializeComponent();
            var componentFactory = App.provider.GetService<IComponentFactory>();
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
            AutoCompleteContainer.Children.Add(placeAutoComplete);
            Grid.SetRow(placeAutoComplete, 0);
            Grid.SetColumn(placeAutoComplete, 0);
        }

        private void OnReceivedPlaceDetails(object sender, PlaceDetailResponse e)
        {
            SelectedItemCommand?.Execute(e);
        }

        public static readonly DependencyProperty SelectedItemProperty =
         DependencyProperty.Register(
              nameof(SelectedItemCommand),
              typeof(ICommand),
              typeof(AutoCompleteComponent),
              new PropertyMetadata(null));


        public ICommand SelectedItemCommand
        {
            get => (ICommand)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
    }
}
