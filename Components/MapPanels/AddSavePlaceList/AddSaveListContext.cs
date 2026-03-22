using CommunityToolkit.Mvvm.Messaging;
using IoC_Container;
using IoC_Container.Attributes;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelPlanning.Components.SaveList.Models;
using TravelPlanning.Contracts;
using TravelPlanning.Messages;
using TravelPlanning.Models.DTOs;
using TravelPlanning.Utilties;
using TravelPlanning.Utilties.Navigation;

namespace TravelPlanning.Components.MapPanels.AddSavePlaceList
{
    [Transient]
    [AddINotifyPropertyChangedInterface]

    public class AddSaveListContext: INavigationAware, IAddSaveListComponentView
    {
        private readonly IAddSaveListPresenter _presenter;
        private string _saveListName = "未命名清單";
        public Guid Id { get; set; }
        public string SaveListName { get; set; }
        public bool IsEditing { get; set; }

        public ICommand BeginEditNameCommand => new RelayCommand(() =>
        {
            IsEditing = true;
        });
        public ICommand EndEditNameCommand => new RelayCommand(() =>
        {
            if (string.IsNullOrWhiteSpace(SaveListName))
                SaveListName = _saveListName;
            _presenter.UpdateListName(Id, SaveListName);
            IsEditing = false;
        });
        public ICommand CancelEditNameCommand => new RelayCommand(() =>
        {
            IsEditing = false;
        });
        public int SaveListCount { get; set; }
        public ObservableCollection<SaveListPlaceViewModel> SaveLists { get; set; }

        public AddSaveListContext(IPresenterFactory presenterFactory) 
        {
            _presenter = presenterFactory.CreatePresneter<IAddSaveListPresenter, IAddSaveListComponentView>(this);
        }

        public void RenderList(List<SavePlaceDTO> addSaveListDTOs)
        {
            var placeIds = addSaveListDTOs.Select(x=>x.PlaceId).ToList();
            // 寄送資訊到 FavoritePage 層接受作 map markers 的渲染
            WeakReferenceMessenger.Default.Send(new SaveListPlacesLoadedMessage(placeIds,Id));
            var viewModels = addSaveListDTOs.Select(x=> new SaveListPlaceViewModel 
            {
                Name = x.Name,
                Type = x.Type,
                Rate = x.Rate,
                Cover = x.Photo
            }).ToList();
            SaveLists = new ObservableCollection<SaveListPlaceViewModel>(viewModels);
            SaveListCount = viewModels.Count;
        }

        public void SendAware(object parm)
        {
            var mapLayer = (SaveListViewModel)parm;
            SaveListName = mapLayer.Name;
            Id = mapLayer.MapLayerId;
            _saveListName = mapLayer.Name;
            _presenter.GetMapPlaces(mapLayer.MapLayerId);
        }

        public void AddPlace(AddPlaceInputDTO placeInputDTO) 
        {
            _presenter.AddPlace(placeInputDTO);
        }
    }
}
