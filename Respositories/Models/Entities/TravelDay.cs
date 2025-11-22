namespace TravelPlanning.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TravelDay")]
    public partial class TravelDay
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TravelDay()
        {
            TravelPlaces = new HashSet<TravelPlace>();
        }

        public Guid Id { get; set; }

        public Guid TravelPlanId { get; set; }

        public int DayOrder { get; set; }

        [Column(TypeName = "date")]
        public DateTime TravelDate { get; set; }

        public virtual TravelPlan TravelPlan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TravelPlace> TravelPlaces { get; set; }
    }
}
