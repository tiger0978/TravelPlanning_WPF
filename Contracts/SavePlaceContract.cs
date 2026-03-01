using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanning.Contracts
{
    public interface ISavePlaceComponentView
    {

    }
    public interface ISavePlaceComponentPresenter
    {
        Task GetMapLayers();
    }

}
