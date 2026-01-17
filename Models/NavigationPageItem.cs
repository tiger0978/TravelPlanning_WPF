using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace TravelPlanning.Models
{
    public class NavigationPageItem
    {
        public string Name { get; set; }       
        public SymbolRegular IconKey { get; set; }       
        public Type TargetPageType { get; set; }  

        public NavigationPageItem(string name, SymbolRegular iconKey, Type targetType)
        {
            Name = name;
            IconKey = iconKey;
            TargetPageType = targetType;
        }
    }
}
