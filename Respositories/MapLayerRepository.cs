using GoogleMap.SDK.Core.Utility;
using System;
using System.CodeDom;
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

        public async Task<MapLayerDAO> AddMapLayerAsync(MapLayerDAO mapLayerDAO)
        {
            var entity = Mapper.Map<MapLayerDAO, MapLayer>(mapLayerDAO);
            _db.MapLayers.Add(entity);
            var res = await _db.SaveChangesAsync();
            return res > 0 ? Mapper.Map<MapLayer, MapLayerDAO>(entity) : null;
        }

        public async Task<bool> DeleteMapLayerByIdAsync(Guid id)
        {
            var entity = await _db.MapLayers.FirstOrDefaultAsync(x => x.Id == id);
            var res = _db.MapLayers.Remove(entity);
            return res != null;
        }

        public async Task<MapLayerDAO> GetMapLayerByIdAsync(Guid id)
        {

            var data = await _db.MapLayers.FirstOrDefaultAsync(x => x.Id == id);
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

    }
}
