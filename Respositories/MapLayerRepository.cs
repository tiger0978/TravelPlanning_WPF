using GoogleMap.SDK.Core.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Models.Entities;
using TravelPlanning.Respositories.Models.DAOs;
using TravelPlanning.Respositories.Models.Entities;

namespace TravelPlanning.Respositories
{
    public class MapLayerRepository : IMapLayerRepository
    {
        private readonly DatabaseContext _db;

        public MapLayerRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<MapLayerDAO> GetMapLayerByIdAsync(Guid mapLayerId)
        {

            var data = await _db.MapLayers.FirstOrDefaultAsync(x => x.Id == mapLayerId);
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MapPlace, MapPlaceDAO>();
            });
            var mapper = config.CreateMapper();
            var result = mapper.Map<MapLayerDAO>(data);
            return result;
        }

        public async Task<List<MapLayerDAO>> GetMapLayersAsync()
        {
            var datas = await _db.MapLayers.ToListAsync();
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MapPlace, MapPlaceDAO>();
            });
            var mapper = config.CreateMapper();
            var result = mapper.Map<List<MapLayerDAO>>(datas);
            return result;
        }

        public async Task<List<MapPlaceDAO>> GetMapPlacesAsync(Guid mapLayerId)
        {
            var places = await _db.MapPlaces.Where(x => x.MapLayerId == mapLayerId).ToListAsync();
            return Mapper.Map<MapPlace, MapPlaceDAO>(places).ToList();
        }
    }
}
