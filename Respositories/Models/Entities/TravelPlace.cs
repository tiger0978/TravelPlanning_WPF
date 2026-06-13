namespace TravelPlanning.Models.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TravelPlace")]
    public partial class TravelPlace
    {
        public Guid Id { get; set; }

        public Guid TravelDayId { get; set; }

        [Required]
        [StringLength(200)]
        public string PlaceId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public DateTime TravelTime { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public bool HasArrivedTime { get; set; }
        public int? TrafficDuration { get; set; }
        public int? TrafficType { get; set; }
        public DateTime? LastPlaceLeavingTime { get; set; }
        public virtual TravelDay TravelDay { get; set; }
    }
}
