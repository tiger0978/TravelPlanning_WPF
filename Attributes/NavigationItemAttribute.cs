using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace TravelPlanning.Attributes
{
    internal class NavigationItemAttribute : Attribute
    {
        public NavigationItemAttribute(string name, SymbolRegular iconKey, int order) 
        {
            Name = name;
            IconKey = iconKey;
            Order = order;
        }

        public string Name { get; set; }
        public SymbolRegular IconKey { get; set; }
        public int Order { get; set; }
    }
}
