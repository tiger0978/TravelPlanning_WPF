using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Models.Entities;

namespace TravelPlanning.Respositories.Models.DAOs
{
    public class TravelDayDAO
    {
        public Guid Id { get; set; }
        public Guid TravelPlanId { get; set; }
        public int DayOrder { get; set; }
        public DateTime TravelDate { get; set; }
        public List<TravelPlaceDAO> TravelPlaces { get; set; }
    }
}
