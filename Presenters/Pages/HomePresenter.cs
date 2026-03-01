using System;
using System.Linq;
using System.Threading.Tasks;
using TravelPlanning.Contracts;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.Respositories;

namespace TravelPlanning.Presenters.Pages
{
    public class HomePresenter : IHomePresenter
    {
        private IHomePage _homeView;
        private ITravelRepository _travelRepository;
        public HomePresenter(IHomePage homeView, ITravelRepository travelRepository) 
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
