using GoogleMap.SDK.Contract;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TravelPlanning.Utilties;
using Wpf.Ui.Controls;

namespace TravelPlanning.Components.MapPanels.PlanRoutePanel.Models
{
    [AddINotifyPropertyChangedInterface]
    public class RouteViewModel
    {
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Distance { get; set; }
        public bool IsExpanded { get; set; }
        public int Index { get; set; }
        public SymbolRegular IconKey { get; set; }
        public ICommand ToggleExpandCommand { get; }
        public ObservableCollection<RouteDetailViewModel> RouteDetails { get; set; }


        public RouteViewModel(string name, string duration, string distance, SymbolRegular iconKey, int index, IGMap gmap)
        {
            Name = name;
            Duration = duration;
            Distance = distance;
            RouteDetails = new ObservableCollection<RouteDetailViewModel>();
            IconKey = iconKey;
            Index = index;
            ToggleExpandCommand = new RelayCommand(() =>
            {
                IsExpanded = !IsExpanded;
                gmap.ActivateRoute(index);
            });
        }
    }

    public class RouteDetailViewModel 
    {
        public string Name { get; set; }
        public string Duration { get; set; }
        public SymbolRegular IconKey { get; set; }
        public RouteDetailViewModel(string name, string duration, string distance, SymbolRegular iconKey)
        {
            Name = name;
            Duration = $"{duration}({distance})";
            IconKey = iconKey;
        }
    }
}
