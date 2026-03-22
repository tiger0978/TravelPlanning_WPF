using GoogleMap.SDK.Core.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            _db.MapLayers.Remove(entity);
            var res = await _db.SaveChangesAsync();
            return res > 0;
        }

        public async Task<MapLayerDAO> GetMapLayerByIdAsync(Guid id)
        {
            var data = await _db.MapLayers.FirstOrDefaultAsync(x => x.Id == id);
            var mapPlaces = Mapper.Map<MapPlace, MapPlaceDAO>(data.MapPlaces).ToList();
            var result = Mapper.Map<MapLayer,MapLayerDAO>(data);
            result.MapPlaces = mapPlaces;
            return result;
        }

        public async Task<List<MapLayerDAO>> GetMapLayersAsync()
        {
            var datas = await _db.MapLayers.Select(x => new MapLayerDAO()
            {
                IconKey = x.IconKey,
                Id = x.Id,
                Name = x.Name,
                MapPlaces =  x.MapPlaces.Select(y=> new MapPlaceDAO()
                {
                    Id = y.Id,
                    Name = y.Name,
                    MapLayerId = x.Id,
                    PlaceId = y.PlaceId,
                }).ToList()
            }).ToListAsync();
            return datas;
        }

        public async Task<bool> UpdateNameAsync(Guid id, string name)
        {
            var mapLayer = await _db.MapLayers.FirstOrDefaultAsync(x => x.Id == id);
            mapLayer.Name = name;
            var result = await _db.SaveChangesAsync();
            return result > 0;
        }
    }
}
