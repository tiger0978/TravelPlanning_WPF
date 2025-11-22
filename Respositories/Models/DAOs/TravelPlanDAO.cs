using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Models.Entities;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Markup;

namespace TravelPlanning.Respositories.Models.DAOs
{
    public class TravelPlanDAO
    {
        public TravelPlanDAO(string title,string description, DateTime startedDate, int days, string cover) 
        {
            Title = title;
            Description = description;
            StartDate = startedDate;
            EndDate = startedDate.AddDays(days);
            Days = days;
            Cover = cover;
        }
        public TravelPlanDAO() { }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Days { get; set; }
        public string Cover { get; set; }
        public List<TravelDayDAO> TravelDays { get; set; }



    }
}
