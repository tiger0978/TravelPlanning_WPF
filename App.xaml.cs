using GoogleMap.SDK.API;
using GoogleMap.SDK.Core;
using GoogleMap.SDK.UI.WPF;
using IoC_Container;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TravelPlanning.Views;
using Wpf.Ui.Appearance;

namespace TravelPlanning
{
    /// <summary>
    /// App.xaml 的互動邏輯
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var temp = ApplicationThemeManager.GetAppTheme();
            ApplicationThemeManager.Apply(ApplicationTheme.Dark, updateAccent: false);
            var temp2 = ApplicationThemeManager.GetAppTheme();
            var services = new ServiceCollection();
            services.AddGoogleMapCoreRegistration();
            services.AddGoogleMapWPFMapRegistration();
            services.AddTransient<Window, MainTravelWindow>();
            services.AddTransient<Window, CreateTravelWindow>();
            var provides = services.BuildServiceProvider();
            var wpfs = (IEnumerable<Window>)provides.GetService(typeof(IEnumerable<Window>));
            var wpf = wpfs.FirstOrDefault(x=>x.GetType() == typeof(CreateTravelWindow));
            wpf.Show();
        }
    }
}
