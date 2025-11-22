using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Respositories.Models.DAOs;

namespace TravelPlanning.Respositories
{
    public interface IMapLayerRepository
    {
        Task<List<MapLayerDAO>> GetMapLayersAsync();
        Task<MapLayerDAO> GetMapLayerByIdAsync(Guid mapLayerId);
        Task<List<MapPlaceDAO>> GetMapPlacesAsync(Guid mapLayerId);
    }
}
