using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Contracts;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.Respositories;
using TravelPlanning.Respositories.Models.DAOs;

namespace TravelPlanning.Presenters.Components
{
    public class SearchPanelPresenter : ISearchPanelComponentPresenter
    {
        private readonly IMapPlaceRepository _mapPlaceRepository;

        public SearchPanelPresenter(IMapPlaceRepository mapPlaceRepository) 
        {
            _mapPlaceRepository = mapPlaceRepository;
        }

        public async Task SavePlace(SavePlaceDTO savePlaceDto)
        {
            var mapPlace = await _mapPlaceRepository.GetMapPlaceByPlaceIdAsync(savePlaceDto.MapLayerId, savePlaceDto.PlaceId);
            if(mapPlace != null) 
            {
                await _mapPlaceRepository.DeleteMapPlaceByIdAsync(mapPlace.Id);
            }
            var mapPlaceDAO = new MapPlaceDAO()
            {
                MapLayerId = savePlaceDto.MapLayerId,
                Name = savePlaceDto.Name,
                PlaceId = savePlaceDto.PlaceId,
            };
            await _mapPlaceRepository.AddMapPlaceAsync(mapPlaceDAO);
        }
    }
}
