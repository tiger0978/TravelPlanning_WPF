using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Contracts;
using TravelPlanning.Respositories;

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


        public async void GetTravelCards()
        {
            var plans = await _travelRepository.GetTravelPlansAsync();
            _homeView.RenderPage(plans);
        }
    }
}
