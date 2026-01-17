using IoC_Container;
using IoC_Container.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TravelPlanning.Components.MapPanels.SavePlacePanel
{
    [Transient]
    public class SavePlacePanelContext
    {
        private readonly IComponentFactory _componentFactory;
        public ObservableCollection<Button> buttons { get; set; } = new ObservableCollection<Button>()
        {
            new Button(){ Tag = "A" },
            new Button(){ Tag = "B" },
            new Button(){ Tag = "C" },
            new Button(){ Tag = "D" },
        };

        public SavePlacePanelContext(IComponentFactory componentFactory) 
        {
            _componentFactory = componentFactory;
        }
    }
}
