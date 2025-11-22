using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using TravelPlanning.Models.Entities;

namespace TravelPlanning.Respositories.Models.Entities
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=DatabaseContext")
        {
        }

        public virtual DbSet<MapLayer> MapLayers { get; set; }
        public virtual DbSet<MapPlace> MapPlaces { get; set; }
        public virtual DbSet<TravelDay> TravelDays { get; set; }
        public virtual DbSet<TravelPlace> TravelPlaces { get; set; }
        public virtual DbSet<TravelPlan> TravelPlans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TravelPlan>()
                .Property(e => e.Cover)
                .IsUnicode(false);
        }
    }
}
