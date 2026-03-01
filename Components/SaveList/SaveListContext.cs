using IoC_Container;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelPlanning.Components.SaveList.Models;
using TravelPlanning.Contracts;
using TravelPlanning.Respositories.Models.DAOs;
using Wpf.Ui.Controls;

namespace TravelPlanning.Components.SaveList
{
    [AddINotifyPropertyChangedInterface]
    public class SaveListContext : ISaveListComponentView
    {
        public ObservableCollection<SaveListViewModel> SaveLists { get; set; }

        public SaveListContext()
        {
            IPresenterFactory presenterFactory = App.provider.GetService<IPresenterFactory>();
            var presenter = presenterFactory.CreatePresneter<ISaveListComponentPresenter, ISaveListComponentView>(this);
            presenter.GetMapLayers();
        }

        public void MapLayerResponse(List<MapLayerDAO> mapLayers)
        {
            SaveLists = new ObservableCollection<SaveListViewModel>(mapLayers.Select(x => new SaveListViewModel
            {
                MapLayerId = x.Id,
                Name = x.Name,
                IconKey = (SymbolRegular)Enum.Parse(typeof(SymbolRegular), x.IconKey),
                Description = $"{x.MapPlaces.Count} 個景點"
            }).ToList());
        }
    }
}
