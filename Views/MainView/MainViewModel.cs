using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Views.Pages.Home;
using Wpf.Ui.Controls;

namespace TravelPlanning.Views.MainTravel
{
    public class MainViewModel
    {
        public ObservableCollection<NavigationViewItem> MenuItems { get; }

        public MainViewModel()
        {
            MenuItems = new ObservableCollection<NavigationViewItem>();
        }

        public void InitialNavigationItems(List<NavigationViewItem> navigations)
        {
            this.MenuItems.Clear();
            foreach (var navigationItem in navigations)
            {
                this.MenuItems.Add(navigationItem);
            }
        }
    }
}
