using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace TravelPlanning.Components.SaveList.Models
{
    public class SaveListViewModel
    {
        public Guid MapLayerId { get; set; }
        public string Name { get; set; }
        public SymbolRegular IconKey { get; set; }
        public string Description { get; set; }
    }
}
