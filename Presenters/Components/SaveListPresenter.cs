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
    public class SaveListPresenter : ISaveListPresenter
    {
        private readonly IMapLayerRepository _mapLayerRepository;
        public SaveListPresenter(IMapLayerRepository mapLayerRepository) 
        {
            _mapLayerRepository = mapLayerRepository;
        }
        public async Task<Guid> AddMapLayer(SaveListDTO saveListDTO)
        {

            var mapLayerDAO = new MapLayerDAO()
            {
                Id = saveListDTO.Id,
                Name = saveListDTO.Name,
                IconKey = saveListDTO.IconKey,
            };
            var mapLayer = await _mapLayerRepository.AddMapLayerAsync(mapLayerDAO);
            return mapLayer.Id;
        }
    }
}
