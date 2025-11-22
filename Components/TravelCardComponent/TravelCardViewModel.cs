using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TravelPlanning.Components.TravelCardComponent
{
    public class TravelCardViewModel
    {
        public string Title { get; set; } = "旅遊標題";
        public DateTime TravelDate { get; set; } = DateTime.Now;
        public BitmapImage Cover { get; set; } = new BitmapImage(new Uri("pack://application:,,,/TravelPlanning;component/Resources/Image/Upload.png", UriKind.Absolute));
    
        public TravelCardViewModel(string title, DateTime travelDate, string cover) { 
            this.Title = title;
            this.TravelDate = travelDate;
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(cover, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();
            Cover = bitmap;
        }
    }
}
