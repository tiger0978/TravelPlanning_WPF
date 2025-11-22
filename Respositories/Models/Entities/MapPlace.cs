namespace TravelPlanning.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MapPlace")]
    public partial class MapPlace
    {
        public Guid Id { get; set; }

        public Guid MapLayerId { get; set; }

        [Required]
        [StringLength(200)]
        public string PlaceId { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public virtual MapLayer MapLayer { get; set; }
    }
}
