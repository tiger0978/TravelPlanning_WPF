using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TravelPlanning.Models.Enums;

namespace TravelPlanning.Components.MapPanels.AddSavePlaceList
{
    public class SaveListPlaceViewModel
    {
        private float? _rate = null;
        public string Name { get; set; }

        public float? Rate
        {
            get => _rate;
            set
            {
                UpdateStars(value);
                _rate = value;
            }
        }
        public string Type { get; set; }
        public ObservableCollection<StarType> Stars { get; } = new ObservableCollection<StarType>();

        public BitmapImage Cover { get; set; } = new BitmapImage(new Uri("https://lh3.googleusercontent.com/gps-cs-s/AHVAwepXToUP4uGwgnDPCR7HtnGZ4JEs3v7JQ4Xrwd2cWs-hSB4md-7-nbPE4-26CviJfyACXaXvjYCJ9e_bWd01bKu1v5yrTabzo3azLBEbkYCbKFM-R-1VFDJ9NNJCfKVrVU6ZQww=w122-h92-k-no"));
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
