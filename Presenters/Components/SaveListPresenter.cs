using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Components.SaveList;
using TravelPlanning.Contracts;
using TravelPlanning.Respositories;

namespace TravelPlanning.Presenters.Components
{
    public class SaveListPresenter : ISaveListComponentPresenter
    {
        private readonly IMapLayerRepository _mapLayerRepository;
        private readonly ISaveListComponentView _view;
        public SaveListPresenter(IMapLayerRepository mapLayerRepository, ISaveListComponentView view)
        {
            _mapLayerRepository = mapLayerRepository;
            _view = view;
        }

        public async Task GetMapLayers() 
        {
            var datas = await _mapLayerRepository.GetMapLayersAsync();
            _view.MapLayerResponse(datas);
        }
    }
}
