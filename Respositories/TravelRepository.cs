using GoogleMap.SDK.Core.Utility;
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
using static GoogleMap.SDK.Contracts.GoogleAPI.Models.Direction.Request.DirectionNewRequest;

namespace TravelPlanning.Respositories
{
    public class TravelRepository : ITravelRepository
    {
        private readonly DatabaseContext _db;

        public TravelRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<TravelPlanDAO> AddTravelPlanAsync(TravelPlanDAO travelPlan)
        {
            var entity = Mapper.Map<TravelPlanDAO,TravelPlan>(travelPlan);
            _db.TravelPlans.Add(entity);
            await _db.SaveChangesAsync();
            var result = Mapper.Map<TravelPlan, TravelPlanDAO>(entity);
            return result;
        }

        public async Task<List<TravelPlaceDAO>> GetTravelPlacesAsync(Guid travelPlanId)
        {
            var plan = await _db.TravelPlans.FirstOrDefaultAsync(x => x.Id == travelPlanId);
            var places = plan.TravelDays.SelectMany(x => x.TravelPlaces).ToList();
            var result = Mapper.Map<TravelPlace, TravelPlaceDAO>(places).ToList();
            return result;
        }

        public async Task<TravelPlanDAO> GetTravelPlanByIdAsync(Guid travelPlanId)
        {
            var data = await _db.TravelPlans.FirstOrDefaultAsync(x => x.Id == travelPlanId);
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TravelPlace, TravelPlaceDAO>();
                cfg.CreateMap<TravelDay, TravelDayDAO>();
                cfg.CreateMap<TravelPlan, TravelPlanDAO>();
            });
            var mapper = config.CreateMapper();
            var result = mapper.Map<TravelPlanDAO>(data);
            return result;
        }

        public async Task<List<TravelPlanDAO>> GetTravelPlansAsync()
        {
            var datas = await _db.TravelPlans.ToListAsync();
            return Mapper.Map<TravelPlan, TravelPlanDAO>(datas,x=> x.ForMember(y=>y.TravelDays,z=>z.Ignore())).ToList();
        }
    }
}
