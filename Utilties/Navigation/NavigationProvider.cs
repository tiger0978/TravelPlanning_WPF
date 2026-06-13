using IoC_Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using TravelPlanning.Attributes;
using TravelPlanning.Utilties.Navigation;

namespace TravelPlanning.Utilties
{
    public class NavigationProvider
    {
        public ContentControl ContentControl;
        public Frame Frame;

        public Dictionary<Type, UserControl> Pages = new Dictionary<Type, UserControl>();
        private List<TypeInfo> pageItems;
        private readonly IComponentFactory componentFactory;

        public NavigationProvider(IComponentFactory componentFactory) 
        {
            this.componentFactory = componentFactory;
            pageItems = Assembly.GetExecutingAssembly().DefinedTypes
              .Where(x => x.FullName.Contains("TravelPlanning.Components.MapPanels")
              || x.FullName.Contains("TravelPlanning.Views.Pages.SearchPlace"))
              .ToList();
        }

        public void SetControl(ContentControl control) 
        {
            ContentControl = control;
        }
        public void SetFrame(Frame frame)
        {
            Frame = frame;
        }

        public void ClearControl() 
        {
            ContentControl.Content = null;
        }

        public UserControl Navigate(Type pageType, object parm)
        {
            var item = pageItems.FirstOrDefault(x => x == pageType);
            var userControl = (UserControl)componentFactory.Create(item);
            if (parm != null && userControl.DataContext is INavigationAware aware)
            {
                aware.SendAware(parm);
            }
            this.ContentControl.Content = userControl;
            return userControl;
        }
        public Page NavigatePage(Type pageType, object parm)
        {
            var item = pageItems.FirstOrDefault(x => x == pageType);
            var page = (Page)componentFactory.Create(item);
            if (parm != null && page.DataContext is INavigationAware aware)
            {
                aware.SendAware(parm);
            }
            this.Frame.Content = page;
            return page;
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
