using IoC_Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using TravelPlanning.Attributes;
using TravelPlanning.Components.MapPanels.SearchPanel;
using TravelPlanning.Models;
using TravelPlanning.Utilties.Navigation;

namespace TravelPlanning.Utilties
{
    public class NavigationProvider
    {
        public ContentControl ContentControl;
        public Dictionary<Type, UserControl> Pages = new Dictionary<Type, UserControl>();
        private List<TypeInfo> pageItems;
        private readonly IComponentFactory componentFactory;

        public NavigationProvider(IComponentFactory componentFactory) 
        {
            this.componentFactory = componentFactory;
            pageItems = Assembly.GetExecutingAssembly().DefinedTypes
              .Where(x => x.FullName.Contains("TravelPlanning.Components.MapPanels"))
              .ToList();
        }

        public void SetControl(ContentControl control) 
        {
            ContentControl = control;
        }


        public void Navigate(Type pageType, object parm)
        {
            if (!Pages.TryGetValue(pageType, out UserControl userControl))
            {
                var item = pageItems.FirstOrDefault(x => x == pageType);
                userControl = (UserControl)componentFactory.Create(item);

                Pages.Add(item, userControl);
            }

            if (userControl is INavigationAware aware)
            {
                aware.SendAware(parm);
            }
            this.ContentControl.Content = userControl;
        }
        public static List<T> GetPages<T>(string typeNamspace)
        {
            List<T> pages = Assembly.GetExecutingAssembly().DefinedTypes
               .Where(x => x.FullName.Contains(typeNamspace))
               .Select(x =>
               {
                   var itemAttribute = x.GetCustomAttribute<NavigationItemAttribute>();
                   if (itemAttribute == null) return default(T);
                   var page = (T)Activator.CreateInstance(typeof(T), itemAttribute.Name, itemAttribute.IconKey, x.AsType());
                   return page;
               }).Where(x => x != null).ToList();
            return pages;
        }

    }
}
