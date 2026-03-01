using CommunityToolkit.Mvvm.ComponentModel;
using GoogleMap.SDK.Contracts.Commons.Enums;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace TravelPlanning.Components.MapPanels.PlanRoutePanel.Models
{
    [AddINotifyPropertyChangedInterface]
    public class TrafficViewModel
    {
        public TrafficMode Mode { get; set; }
        public SymbolRegular IconKey { get; set; }
        public bool IsSelected { get; set; }

        public TrafficViewModel(TrafficMode mode, SymbolRegular iconKey, bool isSelected) 
        {
            Mode = mode;
            IconKey = iconKey;
            IsSelected = isSelected;
        }
    }
}
