using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TravelPlanning.Attributes;

namespace TravelPlanning.Utilties
{
    public static class NavigationPageProvider 
    {

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
