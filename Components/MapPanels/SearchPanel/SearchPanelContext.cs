using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using IoC_Container;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using TravelPlanning.Models.Enums;

namespace TravelPlanning.Components.MapPanels.SearchPanel
{
    [AddINotifyPropertyChangedInterface]
    public class SearchPanelContext 
    {
        private float? _rate = null;
        private IComponentFactory _componentFactory;

        public string PlaceName { get; set; }
        public float? Rate { 
            get => _rate; 
            set
            {
                UpdateStars(value);
                _rate = value;
            }
        }

        public ObservableCollection<StarType> Stars { get;
        }
            = new ObservableCollection<StarType>();
        public string Type { get; set; } = "test";
        public string Addresses { get; set; }
        public string Phone { get; set; }
        public List<string> OpeningTime { get; set; }
        public BitmapImage Photo { get; set; }
        public ICommand ReceivedPlaceCommand { get; set; }
        IGoogleAPIContext _apiContext;

        public SearchPanelContext(IGoogleAPIContext apiContext, IComponentFactory componentFactory) 
        {
            _componentFactory = componentFactory;
            _apiContext = apiContext;
        }

        public void RenderModel(BitmapImage photo, string placeName, string addresses, string phone, string type, float rate, List<string> openingTime) 
        {
            Photo = photo;
            PlaceName = placeName;
            Addresses = addresses;
            Phone = phone;
            Type = type;
            Rate = rate;
            OpeningTime = openingTime;
        }


        public async void RenderModel(PlaceDetailResponse placeDetailResponse)
        {
            var data = placeDetailResponse;
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
