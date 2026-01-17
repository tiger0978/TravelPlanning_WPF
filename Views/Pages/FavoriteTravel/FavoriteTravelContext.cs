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

namespace TravelPlanning.Views.Pages.FavoriteTravel
{
    [Transient]
    public class FavoriteTravelContext : IFavoriteTravelPage
    {
        public PlaceDetailResponse SelectedPlaceDetail { get; set; }
        public ObservableCollection<FavoriteTravelDTO> Favorites { get; set; } = new ObservableCollection<FavoriteTravelDTO>();
        public ICommand SaveMapPlaceCommand { get; set; }
        public ICommand DeleteMapPlaceCommand { get; set; }
        public FavoriteTravelContext(IPresenterFactory presenterFactory) 
        {
            var presenter = presenterFactory.CreatePresneter<IFavoriteTravelPresenter, IFavoriteTravelPage>(this);
            SaveMapPlaceCommand = new RelayCommand(() =>
            {
                var mapPlaceDTO = new FavoriteTravelDTO()
                {
                    PlaceId = SelectedPlaceDetail.result.place_id
                };
            });
        }
    }
}
