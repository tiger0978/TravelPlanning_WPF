using System;
using Wpf.Ui.Controls;

namespace TravelPlanning.Components.SaveList.Models
{
    public class SaveListViewModel
    {
        public Guid MapLayerId { get; set; }
        public string Name { get; set; }
        public SymbolRegular IconKey { get; set; }
        public string Description { get; set; }
        public bool IsHidden { get; set; }
        public string VisibilityText => IsHidden ? "顯示此清單" : "隱藏此清單";
    }
}
