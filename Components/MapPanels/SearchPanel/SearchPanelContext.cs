using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using IoC_Container;
using IoC_Container.Attributes;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TravelPlanning.Components.SaveList;
using TravelPlanning.Components.SaveList.Models;
using TravelPlanning.Contracts;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.Models.Enums;
using TravelPlanning.Utilties;

namespace TravelPlanning.Components.MapPanels.SearchPanel
{
    [Transient]
    [AddINotifyPropertyChangedInterface]
    public class SearchPanelContext : ISearchPanelComponentView
    {
        private readonly ISearchPanelComponentPresenter _presenter;
        private float? _rate = null;
        private string _placeId;

        public string PlaceName { get; set; }
        public float? Rate { 
            get => _rate; 
            set
            {
                UpdateStars(value);
                _rate = value;
            }
        }

        public ObservableCollection<StarType> Stars { get; } = new ObservableCollection<StarType>();
        public string Type { get; set; } = "test";
        public string Addresses { get; set; }
        public string Phone { get; set; }
        public List<string> OpeningTime { get; set; }
        public BitmapImage Photo { get; set; }
        public bool ShowPopup { get; set; } = true;

        public ICommand ReceivedPlaceCommand { get; set; }
        public ICommand ChangePageCommand { get; set; }
        public ICommand RoutePlanCommand { get; set; }
        public ICommand SelectedItemCommand { get; set; }

        IGoogleAPIContext _apiContext;

        public SearchPanelContext(IGoogleAPIContext apiContext,IPresenterFactory presenterFactory, 
            IComponentFactory componentFactory, NavigationProvider navigationProvider) 
        {
            _apiContext = apiContext;
            _presenter = presenterFactory.CreatePresneter<ISearchPanelComponentPresenter, ISearchPanelComponentView>(this);

            this.ChangePageCommand = new RelayCommand(() =>
            {
                ShowPopup = !ShowPopup;
            });

            this.SelectedItemCommand = new RelayCommand<SaveListViewModel>(x =>
            {
                var savePlaceDto = new SavePlaceDto()
                {
                    MapLayerId = x.MapLayerId,
                    Name = x.Name,
                    PlaceId = _placeId
                };
                _presenter.SavePlace(savePlaceDto);
            });
            this.RoutePlanCommand = new RelayCommand<Type>(x =>
            {
                navigationProvider.Navigate(x, null);
            });
        }


        public async void RenderModel(PlaceDetailResponse placeDetailResponse)
        {
            var data = placeDetailResponse;
            _placeId = placeDetailResponse.result.place_id;
            var bytes = await _apiContext.Place.PlacePhotoAsync(data.result.photos[0].photo_reference, 450);
            var image = new BitmapImage();
            using (var ms = new MemoryStream(bytes))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // 很重要
                image.StreamSource = ms;
                image.EndInit();
                image.Freeze(); // 跨執行緒安全
            }

            Photo = image;
            PlaceName = data.result.name;
            Addresses = data.result.formatted_address;
            Phone = data.result.formatted_phone_number;
            Type = data.result.types[0];
            Rate = data.result.rating;
            OpeningTime = data.result.opening_hours?.weekday_text?.ToList();
        }


        private void UpdateStars(float? rate)
        {
            Stars.Clear();

            if (rate == null) return;
            for (int i = 1; i <= 5; i++)
            {
                if (rate >= i)
                {
                    Stars.Add(StarType.Full);
                }
                else if (rate >= i - 0.5f)
                {
                    Stars.Add(StarType.Half);
                }
                else
                {
                    Stars.Add(StarType.Empty);
                }
            }
        }
    }
}
