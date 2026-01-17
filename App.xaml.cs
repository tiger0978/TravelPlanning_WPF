using GoogleMap.SDK.Core;
using GoogleMap.SDK.UI.WPF;
using IoC_Container;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using TravelPlanning.Extensions;
using TravelPlanning.Respositories;
using TravelPlanning.Respositories.Models.Entities;
using TravelPlanning.Views;
using Wpf.Ui;
using Wpf.Ui.DependencyInjection;

namespace TravelPlanning
{
    /// <summary>
    /// App.xaml 的互動邏輯
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var services = new ServiceCollection();
            services.AddGoogleMapCoreRegistration();
            services.AddGoogleMapWPFMapRegistration();
            services.AddTransient<Window, MainTravelWindow>();
            services.AddTransient<ITravelRepository,TravelRepository>();
            services.AddTransient<DatabaseContext, DatabaseContext>();
            services.AddNavigationViewPageProvider();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IContentDialogService, ContentDialogService>();
            // services.AddSingleton<IPageService>
            services.AddPage();

            services.AutoInjectMVP(Assembly.GetExecutingAssembly());
            services.AutoInjectComponent(Assembly.GetExecutingAssembly());

            //services.AddTransient<Page, CreateTravel>();
            //services.AddTransient<Page, FavoriteTravel>();
            var provides = services.BuildServiceProvider();

            //var repo = provides.GetService<ITravelRepository>();
            //await repo.GetTravelPlacesAsync(Guid.Parse("52EE1A77-764A-473E-9222-DE738BDC3438"));

            var wpfs = (IEnumerable<Window>)provides.GetService(typeof(IEnumerable<Window>));
            var wpf = wpfs.FirstOrDefault(x=>x.GetType() == typeof(MainTravelWindow));
            wpf.Show();
        }
    }
}
