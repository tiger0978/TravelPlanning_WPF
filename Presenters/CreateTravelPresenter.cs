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
using TravelPlanning.Extensions;
using TravelPlanning.Respositories;
using TravelPlanning.Respositories.Models.DAOs;

namespace TravelPlanning.Presenters
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
            int newWidth = 190;
            int newHeight = 340;
            var priveiwImage = travelPlanDto.Cover.Resize(newWidth, newHeight);
            priveiwImage.SaveJpeg(Path.Combine(ImageRootPath, $"cover_{priveiwImage.ToBase64()}"),80);

            string cover = travelPlanDto.Cover.ToBase64();
            travelPlanDto.Cover.SaveJpeg(Path.Combine(ImageRootPath, cover), 80);

            var travelPlan = new TravelPlanDAO(travelPlanDto.Title, travelPlanDto.Description, travelPlanDto.StartedDate, travelPlanDto.Days, cover);
            await _travelRepository.AddTravelPlanAsync(travelPlan);
        }
    }
}
