using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPlanning.Contracts;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.Models.DTOs;
using TravelPlanning.Respositories;
using TravelPlanning.Respositories.Models.DAOs;
using TravelPlanning.Views.Pages.FavoriteTravel;

namespace TravelPlanning.Presenters.Pages
{
    public class FavoriteTravelPresenter : IFavoriteTravelPresenter
    {
        private readonly IMapPlaceRepository _mapPlaceRepository;
        private readonly IMapLayerRepository _mapLayerRepository;
        private readonly IFavoriteTravelPage _favoriteTravelPage;

        public FavoriteTravelPresenter(IMapLayerRepository mapLayerRepository, IMapPlaceRepository mapPlaceRepository,
            IFavoriteTravelPage favoriteTravelPage)
        {
            _mapLayerRepository = mapLayerRepository;
            _mapPlaceRepository = mapPlaceRepository;
            _favoriteTravelPage = favoriteTravelPage;
        }

        public async Task CreateMapLayerAsync(string name)
        {
            var mapLayerDAO = new MapLayerDAO(name);
            var mapLayer = await _mapLayerRepository.AddMapLayerAsync(mapLayerDAO);
        }

        public async Task GetMapLayersAsync()
        {
            var mapLayers = await _mapLayerRepository.GetMapLayersAsync();
         
        }

        public async Task GetMapPlacesByMapLayerId(Guid mapLayerId)
        {
            var mapPlaces = await _mapPlaceRepository.GetMapPlacesByMapperIdAsync(mapLayerId);
        }

        public async Task CreateMapPlaceAsync(FavoriteTravelDTO favoriteTravelDTO)
        {
            var mapLayer = await _mapLayerRepository.GetMapLayerByIdAsync(favoriteTravelDTO.MapLayerId);
            //if (mapLayer == null)
            //{
            //    var mapLayerDAO = new MapLayerDAO(favoriteTravelDTO.MapLayerName);
            //    mapLayer = await _mapLayerRepository.AddMapLayerAsync(mapLayerDAO);
            //}
            favoriteTravelDTO.MapLayerId = mapLayer.Id;
            var mapPlaceDAO = new MapPlaceDAO(favoriteTravelDTO.MapLayerId, favoriteTravelDTO.PlaceId, favoriteTravelDTO.Name);
            mapPlaceDAO = await _mapPlaceRepository.AddMapPlaceAsync(mapPlaceDAO);
        }

        public async Task<bool> DeleteMapPlaceByIdAsync(Guid id)
        {
             return await _mapPlaceRepository.DeleteMapPlaceByIdAsync(id);
        }

        public async Task<bool> DeleteMapPlaceByMapLayerIdAsync(Guid maplayerId)
        {
            return await _mapPlaceRepository.DeleteMapPlacesByMapperLayerId(maplayerId);
        }

        public async Task<List<MapPlaceDTO>> GetAllMapPlacesASync()
        {
            var mapLayers = await _mapLayerRepository.GetMapLayersAsync();
            var mapPlaces = mapLayers
                .SelectMany(x => x.MapPlaces)
                .Select(y => new MapPlaceDTO
                {
                    Id = y.Id,
                    MapLayerId = y.MapLayerId,
                    Name = y.Name,
                    PlaceId = y.PlaceId,
                }).ToList();
            return mapPlaces;
        }
    }
}
