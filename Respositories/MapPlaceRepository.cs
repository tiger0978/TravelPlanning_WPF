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
    public class MapPlaceRepository : IMapPlaceRepository
    {
        private readonly DatabaseContext _db;

        public MapPlaceRepository(DatabaseContext db)
        {
            _db = db;
        }
        public async Task<MapPlaceDAO> AddMapPlaceAsync(MapPlaceDAO mapPlace)
        {
            var entity = Mapper.Map<MapPlaceDAO, MapPlace>(mapPlace);
            var res = _db.MapPlaces.Add(entity);
            await _db.SaveChangesAsync();
            return Mapper.Map<MapPlace, MapPlaceDAO>(res);
        }

        public async Task<bool> DeleteMapPlaceByIdAsync(Guid id)
        {
            var entity = await _db.MapPlaces.FirstOrDefaultAsync(x => x.Id == id);
            var res = _db.MapPlaces.Remove(entity);
            return res != null;
        }

        public async Task<bool> DeleteMapPlacesByMapperLayerId(Guid id)
        {
            var entitys = await _db.MapPlaces.Where(x => x.MapLayerId == id).ToListAsync();
            var res = _db.MapPlaces.RemoveRange(entitys);
            return res != null;
        }

        public async Task<MapPlaceDAO> GetMapPlaceByPlaceIdAsync(Guid mapLayerId, string placeId) 
        {
            var entity = await _db.MapPlaces.FirstOrDefaultAsync(
                x => x.MapLayerId == mapLayerId
                && x.PlaceId == placeId);
            var result = Mapper.Map<MapPlace, MapPlaceDAO>(entity);
            return result;
        }

        public async Task<List<MapPlaceDAO>> GetMapPlacesAsync()
        {
            var entities = await _db.MapPlaces.ToListAsync();
            var result = Mapper.Map<MapPlace, MapPlaceDAO>(entities).ToList();
            return result;
        }

        public async Task<List<MapPlaceDAO>> GetMapPlacesByMapperIdAsync(Guid mapperId)
        {
            var entities = await _db.MapPlaces.Where(x => x.MapLayerId == mapperId).ToListAsync();
            var result = Mapper.Map<MapPlace, MapPlaceDAO>(entities).ToList();
            return result;
        }
    }
}
