using System;
using System.Collections.Generic;
using System.Linq;
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
            StartedDate = startDate;
            Cover = cover;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public int Days { get; set; } 
        public DateTime StartedDate { get; set; } 
        public BitmapImage Cover { get; set; } 
    }
}
