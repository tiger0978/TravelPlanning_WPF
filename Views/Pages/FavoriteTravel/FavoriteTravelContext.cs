using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using IoC_Container;
using IoC_Container.Attributes;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using TravelPlanning.Components.MapPanels;
using TravelPlanning.Components.MapPanels.SearchPanel;
using TravelPlanning.Contracts;
using TravelPlanning.Models.DTOs;

namespace TravelPlanning.Views.Pages.FavoriteTravel
{
    [Transient]
    [AddINotifyPropertyChangedInterface]
    public class FavoriteTravelContext : IFavoriteTravelPage
    {
        public PlaceDetailResponse SelectedPlaceDetail { get; set; }
        private readonly IFavoriteTravelPresenter _presenter;
        //public ObservableCollection<FavoriteTravelDTO> Favorites { get; set; } = new ObservableCollection<FavoriteTravelDTO>();
        //public ICommand SaveMapPlaceCommand { get; set; }
        //public ICommand DeleteMapPlaceCommand { get; set; }

        public List<MapPlaceDTO> MapPlaces { get; set; }
        public bool IsPopupOpen { get; set; }
        public UserControl PopupContent { get; set; }

        public MapPanelContext MapPanelContext { get; set; }
        public FavoriteTravelContext(IPresenterFactory presenterFactory, IComponentFactory componentFactory, MapPanelContext mapPanelContext) 
        {
            var presenter = presenterFactory.CreatePresneter<IFavoriteTravelPresenter, IFavoriteTravelPage>(this);
            _presenter = presenter;
            MapPanelContext = mapPanelContext;
            PopupContent = componentFactory.Create<SearchPanelComponent>();
        }

        public async Task<List<MapPlaceDTO>> GetAllMapPlaces()
        {
            return await _presenter.GetAllMapPlacesASync();
        }

        public async Task<List<MapPlaceDTO>> GetMapPlacesByLayerId(Guid mapLayerId) 
        {
            return await _presenter.GetMapPlacesByMapLayerId(mapLayerId);
        }
    }
}
