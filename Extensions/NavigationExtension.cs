using IoC_Container;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TravelPlanning.Attributes;

namespace TravelPlanning.Extensions
{
    internal static class NavigationExtension
    {
        public static void AddPage(this IoC_Container.ServiceCollection serivces)
        {

            Assembly.GetExecutingAssembly().DefinedTypes
                .Where(x => x.BaseType == typeof(Page) && x.GetCustomAttribute<NavigationItemAttribute>() != null)
                .OrderBy(x=> x.GetCustomAttribute<NavigationItemAttribute>().Order)
                .ToList()
                .ForEach(type =>
                {
                    serivces.AddSingleton(typeof(Page), type);
                    serivces.AddSingleton(type, type);
                });
            
        }
    }
}
