using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TravelPlanning.Components.TravelCardComponent
{
    public class TravelCardViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "旅遊標題";
        public DateTime TravelDate { get; set; } = DateTime.Now;
        public BitmapImage Cover { get; set; } = new BitmapImage(new Uri("pack://application:,,,/TravelPlanning;component/Resources/Image/Upload.png", UriKind.Absolute));


        public TravelCardViewModel() { }

        public TravelCardViewModel(Guid id, string title, DateTime travelDate, BitmapImage cover) { 
            Id = id;
            Title = title;
            TravelDate = travelDate;
            Cover = cover;
        }
    }
}
