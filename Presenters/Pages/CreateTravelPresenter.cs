using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TravelPlanning.Contracts;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.EventHandlers;
using TravelPlanning.Extensions;
using TravelPlanning.Respositories;
using TravelPlanning.Respositories.Models.DAOs;

namespace TravelPlanning.Presenters.Pages
{
    public class CreateTravelPresenter : ICreateTravelPresenter
    {
        private ITravelRepository _travelRepository;
        private string ImageRootPath = "C:\\Users\\user\\source\\repos\\TravelPlanResource";
        public CreateTravelPresenter(ITravelRepository travelRepository)
        {
            _travelRepository = travelRepository;
        }

        public async Task AddTravelPlan(TravelPlanDTO travelPlanDto)
        {
            int newWidth = 340;
            int newHeight = 190;
            var priveiwImage = travelPlanDto.Cover.Resize(newWidth, newHeight);
            var imageName = $"{Guid.NewGuid().ToString()}.jpeg";  
            priveiwImage.SaveJpeg(Path.Combine(ImageRootPath, $"cover_{imageName}"), 80);

            string cover = Path.Combine(ImageRootPath, imageName);
            travelPlanDto.Cover.SaveJpeg(Path.Combine(ImageRootPath, cover), 80);

            var travelPlan = new TravelPlanDAO(travelPlanDto.Title, travelPlanDto.Description, travelPlanDto.StartDate, travelPlanDto.Days, cover);
            var result = await _travelRepository.AddTravelPlanAsync(travelPlan);
            TravelCardHandler.RenderTravelCard(new TravelPlanDTO(travelPlan.Id, travelPlan.Title, travelPlan.StartDate, travelPlan.Cover));
        }
    }
}
