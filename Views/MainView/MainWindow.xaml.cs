using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TravelPlanning.Attributes;
using TravelPlanning.Views.MainTravel;
using TravelPlanning.Views.Pages.Home;
using Wpf.Ui;
using Wpf.Ui.Abstractions;
using Wpf.Ui.Controls;

namespace TravelPlanning.Views
{
    /// <summary>
    /// MainTravelWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainTravelWindow//: INavigationWindow
    {
        MainViewModel viewModel { get; set; } = new MainViewModel();
        public MainTravelWindow(IEnumerable<Page> pages, INavigationService navigationService)
        {
            InitializeComponent();
            DataContext = viewModel;
            
            var navigationItems = pages.Select(x=> 
            {
                var itemAttribute = x.GetType().GetCustomAttribute<NavigationItemAttribute>();
                if(itemAttribute == null) return null;
               // new NavigationViewItem(itemAttribute.Name, itemAttribute.IconKey,);
                var navigationItem = new NavigationViewItem(itemAttribute.Name,itemAttribute.IconKey, x.GetType());
                return navigationItem;
            }).Where(x=>x != null).ToList();
            viewModel.InitialNavigationItems(navigationItems);
            Loaded += (s, e) => navigationView.Navigate(typeof(Home));
            navigationService.SetNavigationControl(navigationView);
        }

        public bool Navigate(Type pageType)
        {
            return navigationView.Navigate(pageType);
        }

        public void SetPageService(INavigationViewPageProvider navigationViewPageProvider)
        {
            navigationView.SetPageProviderService(navigationViewPageProvider);

        }

        public void ShowWindow() => Show();

        public void CloseWindow() => Close();

        public INavigationView GetNavigation()
        {
            throw new NotImplementedException();
        }

        public void SetServiceProvider(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}
