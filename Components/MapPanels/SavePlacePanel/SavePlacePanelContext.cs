using IoC_Container;
using IoC_Container.Attributes;
using PropertyChanged;
using System.Windows.Input;
using TravelPlanning.Components.MapPanels.AddSavePlaceList;
using TravelPlanning.Components.SaveList.Models;
using TravelPlanning.Contracts;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.Utilties;
using Wpf.Ui.Controls;

namespace TravelPlanning.Components.MapPanels.SavePlacePanel
{
    [Transient]
    [AddINotifyPropertyChangedInterface]
    public class SavePlacePanelContext : ISaveListView
    {
        public string Name { get; set; }
        public SymbolRegular IconKey { get; set; }
        public int PlaceCount { get; set; }
        public ICommand SelectedItemCommand { get; set; }
        public ICommand AddListCommand { get; set; }

        public SavePlacePanelContext(IComponentFactory componentFactory,IPresenterFactory presenterFactory, 
            NavigationProvider navigationProvider) 
        {
            var presenter = presenterFactory.CreatePresneter<ISaveListPresenter, ISaveListView>(this);

            SelectedItemCommand = new RelayCommand<SaveListViewModel>(x =>
            {
                navigationProvider.Navigate(typeof(AddSaveListComponent), x);
            });

            AddListCommand = new RelayCommand(async x => 
            {
                SaveListDTO saveListDTO = new SaveListDTO();
                var mapLayerId = await presenter.AddMapLayer(saveListDTO);
                var saveListViewModel = new SaveListViewModel()
                {
                    MapLayerId = mapLayerId,
                    Name = saveListDTO.Name,
                };
                navigationProvider.Navigate(typeof(AddSaveListComponent), saveListViewModel);
            });
        }
    }
}
