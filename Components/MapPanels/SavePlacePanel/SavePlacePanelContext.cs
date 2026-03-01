using IoC_Container;
using IoC_Container.Attributes;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Policy;
using System.Windows.Controls;
using System.Windows.Input;
using TravelPlanning.Components.MapPanels.AddSavePlaceList;
using TravelPlanning.Components.SaveList.Models;
using TravelPlanning.Utilties;
using Wpf.Ui.Controls;

namespace TravelPlanning.Components.MapPanels.SavePlacePanel
{
    [Transient]
    [AddINotifyPropertyChangedInterface]
    public class SavePlacePanelContext
    {
        public string Name { get; set; }
        public SymbolRegular IconKey { get; set; }
        public int PlaceCount { get; set; }
        public ICommand SelectedItemCommand { get; set; }



       

        public SavePlacePanelContext(IComponentFactory componentFactory, NavigationProvider navigationProvider) 
        {
            SelectedItemCommand = new RelayCommand<SaveListViewModel>(x =>
            {
                navigationProvider.Navigate(typeof(AddSaveListComponent), null);
                Debug.WriteLine(x.Name);
            });
        }
    }
}
