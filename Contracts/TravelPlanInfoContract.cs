using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Views.Pages.TravelPlanInfo.Models;

namespace TravelPlanning.Contracts
{
    public interface ITravelPlanInfoView
    {
        void OnTravelPlanInfoResponse(TravelPlanDTO travelPlanDTO);
        void OnAddTravelPlaceResponse(List<TravelPlaceDTO> travelPlacesDTO);
        void OnDeleteTravelPlaceResponse(List<TravelPlaceDTO> travelPlacesDTO);
        void OnEditTravelPlaceTimeResponse(List<TravelPlaceDTO> travelPlacesDTO);
        void OnAddNewTravelDay(TravelPlanDayDTO travelPlanDayDTO);
    }
    public interface ITravelPlanInfoPresenter
    {
        Task GetTravelPlanInfo(Guid planId);
        Task AddNewPlace(AddTravelPlaceDTO addTravelPlaceDTO);
        Task DeletePlace(DeleteTravelPlaceDTO deleteTravelPlaceDTO);
        Task UpdateTravelTime(TravelPlaceDTO travelPlaceDTO);
        Task UpdateTravelWay(TravelPlaceDTO travelPlaceDTO);
        Task AddNewTravelDay(Guid travelDayId);

    }
   
}
