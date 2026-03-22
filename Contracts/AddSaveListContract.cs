using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Models.DTOs;

namespace TravelPlanning.Contracts
{
    public interface IAddSaveListComponentView
    {
        void RenderList(List<SavePlaceDTO> addSaveListDTOs);
    }
    public interface IAddSaveListPresenter 
    {
        void GetMapPlaces(Guid mapLayerId);
        void UpdateListName (Guid mapLayerId, string name);
        void AddPlace(AddPlaceInputDTO placeDTO);
    }
}
