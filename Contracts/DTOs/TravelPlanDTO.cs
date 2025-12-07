using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TravelPlanning.Contracts.DTOs
{
    public class TravelPlanDTO
    {
        public TravelPlanDTO(string title,string description, int days, DateTime startDate, BitmapImage cover) 
        {
            Title = title;
            Description = description;
            Days = days;
            StartDate = startDate;
            Cover = cover;
        }

        public TravelPlanDTO(Guid id, string title, DateTime startDate, string cover) 
        {
            Id = id;
            Title = title;
            StartDate = startDate;
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(cover, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();
            Cover = bitmap;
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Days { get; set; } 
        public DateTime StartDate { get; set; } 
        public BitmapImage Cover { get; set; } 
    }
}
