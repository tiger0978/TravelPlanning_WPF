using IoC_Container.Attributes;
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
    [Transient]
    public class MainWindowContext
    {
        public ObservableCollection<NavigationViewItem> MenuItems { get; }

        public MainWindowContext()
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
