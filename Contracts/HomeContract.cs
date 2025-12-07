using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.Respositories.Models.DAOs;

namespace TravelPlanning.Contracts
{
    public interface IHomeView 
    {
        void RenderPage(List<TravelPlanDTO> plans);
    }
    public interface IHomePresenter
    {
        Task GetTravelCards();
        Task<bool> DeleteTravelCard(Guid Id);
    }
}
