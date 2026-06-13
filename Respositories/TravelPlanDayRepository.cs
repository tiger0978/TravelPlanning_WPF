using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using TravelPlanning.Models.Entities;
using TravelPlanning.Respositories.Models.DAOs;
using TravelPlanning.Respositories.Models.Entities;

namespace TravelPlanning.Respositories
{
    public class TravelPlanDayRepository : ITravelPlanDayRepository
    {
        private readonly DatabaseContext _db;
        public TravelPlanDayRepository(DatabaseContext db) 
        {
            _db = db;
        }
        public async Task<TravelDayDAO> AddNewTravelDay(Guid travelPlanId)
        {
            var lastDay = await _db.TravelDays
                .Where(x => x.TravelPlanId == travelPlanId)
                .OrderByDescending(x => x.DayOrder)
                .FirstOrDefaultAsync();
            var entity = new TravelDay
            {
                Id = Guid.NewGuid(),
                TravelPlanId = travelPlanId,
                DayOrder = lastDay != null ? lastDay.DayOrder + 1 : 1,
                TravelDate = lastDay != null ? lastDay.TravelDate.AddDays(1) : DateTime.Today
            };
            _db.TravelDays.Add(entity);
            await _db.SaveChangesAsync();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TravelPlace, TravelPlaceDAO>();
                cfg.CreateMap<TravelDay, TravelDayDAO>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<TravelDayDAO>(entity);
        }

        public async Task AddTravelDayAsync(List<TravelDayDAO> travelDays)
        {
            var travelDayEntities = travelDays.Select(x => new TravelDay()
            {
                Id = Guid.NewGuid(),
                TravelPlanId = x.TravelPlanId,
                TravelDate = x.TravelDate,
                DayOrder = x.DayOrder,
            });
            _db.TravelDays.AddRange(travelDayEntities);
            await _db.SaveChangesAsync();
        }
    }
}
