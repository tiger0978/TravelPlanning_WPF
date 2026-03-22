using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Respositories.Models.DAOs;

namespace TravelPlanning.Contracts
{
    public interface ISaveListComponentPresenter
    {
        Task GetMapLayers();
        Task DeleteMapLayers(Guid mapLayerId);
    }
    public interface ISaveListComponentView
    {
        void MapLayerResponse(List<MapLayerDAO> mapLayers);
    }
}
