using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using GoogleMap.SDK.UI.WPF;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using GoogleMap.SDK.Contract.Components.Gmap.Models;
using GoogleMap.SDK.Contracts.GoogleAPI;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using TravelPlanning.Utilties;
using IoC_Container;
using TravelPlanning.Contracts;
using TravelPlanning.Contracts.DTOs;
using System.Collections.ObjectModel;
using IoC_Container.Attributes;
using TravelPlanning.Components.MapPanels.SearchPanel;
using PropertyChanged;
using System.Collections.Generic;
using TravelPlanning.Models.DTOs;
using System.Threading.Tasks;
using TravelPlanning.Components.MapPanels;

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
            //SaveMapPlaceCommand = new RelayCommand(() =>
            //{
            //    var mapPlaceDTO = new FavoriteTravelDTO()
            //    {
            //        PlaceId = SelectedPlaceDetail.result.place_id
            //    };
            //});
            PopupContent = componentFactory.Create<SearchPanelComponent>();
        }

        public async Task<List<MapPlaceDTO>> GetAllMapPlaces()
        {
            return await _presenter.GetAllMapPlacesASync();
        }
    }
}
