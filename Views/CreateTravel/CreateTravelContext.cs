using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TravelPlanning.Views.CreateTravel
{
    [AddINotifyPropertyChangedInterface]
    public class CreateTravelContext
    {
        public string Title { get; set; } = "宜蘭三天兩夜";
        public int Days { get; set; } = 3;
        public DateTime StartedDate { get; set; } = DateTime.Now;
        public BitmapImage Cover { get; set; } = new BitmapImage(new Uri("pack://application:,,,/TravelPlanning;component/Resources/Image/Upload.png", UriKind.Absolute));

    }
}
