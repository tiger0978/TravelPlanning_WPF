namespace TravelPlanning.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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

        public TimeSpan? TravelTime { get; set; }

        public virtual TravelDay TravelDay { get; set; }
    }
}
