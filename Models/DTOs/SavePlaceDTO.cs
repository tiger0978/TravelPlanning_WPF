using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TravelPlanning.Models.DTOs
{
    public class SavePlaceDTO
    {
        public Guid Id { get; set; }
        public Guid MapLayerId { get; set; }
        public string PlaceId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public float Rate { get; set; }
        public BitmapImage Photo { get; set; }
    }
}
