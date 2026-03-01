using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Contracts.DTOs;

namespace TravelPlanning.Contracts
{
    public interface ISearchPanelComponentView
    {

    }
    public interface ISearchPanelComponentPresenter
    {
        Task SavePlace(SavePlaceDto savePlaceDto);
    }
}
