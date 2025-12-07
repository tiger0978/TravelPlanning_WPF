using GoogleMap.SDK.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Contracts;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.Respositories;
using TravelPlanning.Respositories.Models.DAOs;

namespace TravelPlanning.Presenters
{
    public class HomePresenter : IHomePresenter
    {
        private IHomeView _homeView;
        private ITravelRepository _travelRepository;
        public HomePresenter(IHomeView homeView, ITravelRepository travelRepository) 
        {
            _homeView = homeView;
            _travelRepository = travelRepository;
        }

        public async Task GetTravelCards()
        {
            var plans = await _travelRepository.GetTravelPlansAsync();
            var planDtos = plans.Select(x=> new TravelPlanDTO(x.Id, x.Title,x.StartDate,x.Cover)).ToList();
            _homeView.RenderPage(planDtos);
        }

        public async Task<bool> DeleteTravelCard(Guid Id)
        {
            var result = await _travelRepository.DeleteTravelPlanByIdAsync(Id);
            return result;
        }
    }
}
