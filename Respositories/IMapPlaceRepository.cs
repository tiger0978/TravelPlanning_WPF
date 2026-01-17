using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Respositories.Models.DAOs;

namespace TravelPlanning.Respositories
{
    public interface IMapPlaceRepository
    {
        Task<MapPlaceDAO> AddMapPlaceAsync(MapPlaceDAO mapPlace);
        Task<List<MapPlaceDAO>> GetMapPlacesAsync();
        Task<List<MapPlaceDAO>> GetMapPlacesByMapperIdAsync(Guid mapperId);
        Task<bool> DeleteMapPlaceByIdAsync(Guid id);
        Task<bool> DeleteMapPlacesByMapperLayerId(Guid id);
    }
}
