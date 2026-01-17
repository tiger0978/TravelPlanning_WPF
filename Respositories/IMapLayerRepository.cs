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
        Task<MapLayerDAO> GetMapLayerByIdAsync(Guid id);
        Task<MapLayerDAO> AddMapLayerAsync(MapLayerDAO mapLayerDAO);
        Task<bool> DeleteMapLayerByIdAsync(Guid id);
    }
}
