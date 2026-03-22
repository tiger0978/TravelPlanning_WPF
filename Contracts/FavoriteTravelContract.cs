using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.Models.DTOs;

namespace TravelPlanning.Contracts
{
    public interface IFavoriteTravelPage
    {
    }
    public interface IFavoriteTravelPresenter
    {
        Task CreateMapLayerAsync(string name);
        Task GetMapLayersAsync();
        Task<List<MapPlaceDTO>> GetAllMapPlacesASync();
        Task GetMapPlacesByMapLayerId(Guid mapLayerId);
        Task CreateMapPlaceAsync(FavoriteTravelDTO favoriteTravelDTO);
        Task<bool> DeleteMapPlaceByIdAsync(Guid id);
        Task<bool> DeleteMapPlaceByMapLayerIdAsync(Guid maplayerId);
    }
}
