using System;
using System.Threading.Tasks;
using TravelPlanning.Contracts;
using TravelPlanning.Respositories;

namespace TravelPlanning.Presenters.Components
{
    public class SaveListComponentPresenter : ISaveListComponentPresenter
    {
        private readonly IMapLayerRepository _mapLayerRepository;
        private readonly ISaveListComponentView _view;
        public SaveListComponentPresenter(IMapLayerRepository mapLayerRepository, ISaveListComponentView view)
        {
            _mapLayerRepository = mapLayerRepository;
            _view = view;
        }

        public async Task DeleteMapLayers(Guid mapLayerId)
        {
            await _mapLayerRepository.DeleteMapLayerByIdAsync(mapLayerId);
            await GetMapLayers();
        }

        public async Task GetMapLayers() 
        {
            var datas = await _mapLayerRepository.GetMapLayersAsync();
            _view.MapLayerResponse(datas);
        }
    }
}
