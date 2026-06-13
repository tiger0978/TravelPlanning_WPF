using CommunityToolkit.Mvvm.Messaging;
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
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TravelPlanning.Components.SaveList.Models;
using TravelPlanning.Contracts;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.Messages;
using TravelPlanning.Models.Enums;
using TravelPlanning.Utilties;
using TravelPlanning.Views.Pages.SearchPlace.Comment;
using TravelPlanning.Views.Pages.SearchPlace.OverView;

namespace TravelPlanning.Components.MapPanels.SearchPanel
{
    [Transient]
    [AddINotifyPropertyChangedInterface]
    public class SearchPanelContext : ISearchPanelComponentView
    {
        private readonly ISearchPanelComponentPresenter _presenter;
        private PlaceDetailResponse _response;
        private float? _rate = null;
        private string _placeId;
        public NavigationProvider NavigationProvider { get; set; }

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
        public bool ShowPopup { get; set; } = false;

        public ICommand ChangePageCommand { get; set; }
        public ICommand RoutePlanCommand { get; set; }
        public ICommand SelectedItemCommand { get; set; }
        public ICommand ChangeTabCommand { get; set; }

        IGoogleAPIContext _apiContext;

        public SearchPanelContext(IGoogleAPIContext apiContext,IPresenterFactory presenterFactory, 
            IComponentFactory componentFactory, NavigationProvider navigationProvider) 
        {
            NavigationProvider = navigationProvider;
            _apiContext = apiContext;
            _presenter = presenterFactory.CreatePresneter<ISearchPanelComponentPresenter, ISearchPanelComponentView>(this);

            this.ChangePageCommand = new RelayCommand(() =>
            {
                ShowPopup = !ShowPopup;
            });

            this.SelectedItemCommand = new RelayCommand<SaveListViewModel>(x =>
            {
                var savePlaceDto = new SavePlaceDTO()
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

            this.ChangeTabCommand = new RelayCommand<Type>(pageType =>
            {
                if(pageType == typeof(OverViewPage))
                {
                    navigationProvider.NavigatePage(pageType, _response);
                }
                else if(pageType == typeof(CommentPage))
                {
                    navigationProvider.NavigatePage(pageType, _response.result.reviews);
                }
            });
        }


        public async void RenderModel(PlaceDetailResponse placeDetailResponse)
        {
            _response = placeDetailResponse;
            _placeId = placeDetailResponse.result.place_id;
            NavigationProvider.NavigatePage(typeof(OverViewPage), _response);
            var bytes = await _apiContext.Place.PlacePhotoAsync(_response.result.photos[0].photo_reference, 450);
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
            PlaceName = _response.result.name;
            Addresses = _response.result.formatted_address;
            Phone = _response.result.formatted_phone_number;
            Type = _response.result.types[0];
            Rate = _response.result.rating;
            OpeningTime = _response.result.opening_hours?.weekday_text?.ToList();
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
