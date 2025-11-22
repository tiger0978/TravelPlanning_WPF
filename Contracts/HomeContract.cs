using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Respositories.Models.DAOs;

namespace TravelPlanning.Contracts
{
    public interface IHomeView 
    {
        void RenderPage(List<TravelPlanDAO> plans);
    }
    public interface IHomePresenter
    {
        void GetTravelCards();
    }
}
