using System;
using TravelPlanning.Utilties;
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
    public partial class MainTravelWindow
    {
        MainWindowContext ViewModel { get; set; } = new MainWindowContext();
        public MainTravelWindow(INavigationService navigationService)
        {
            InitializeComponent();
            DataContext = ViewModel;
            var pages = NavigationPageProvider.GetPages<NavigationViewItem>("TravelPlanning.Views.Pages");
            ViewModel.InitialNavigationItems(pages);
            Loaded += (s, e) => navigationView.Navigate(typeof(HomePage));
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
