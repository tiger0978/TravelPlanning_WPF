using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
        private readonly ITravelRepository _travelRepository;
        private readonly ITravelPlanDayRepository _travelPlanDayRespository;
        private string ImageRootPath = "C:\\Users\\user\\source\\repos\\TravelPlanResource";
        public CreateTravelPresenter(ITravelRepository travelRepository, ITravelPlanDayRepository travelPlanDayRespository)
        {
            _travelRepository = travelRepository;
            _travelPlanDayRespository = travelPlanDayRespository;
        }

        public async Task AddTravelPlanAsync(TravelPlanDTO travelPlanDto)
        {
            int newWidth = 340;
            int newHeight = 190;
            var priveiwImage = travelPlanDto.Cover.Resize(newWidth, newHeight);
            var imageName = $"{Guid.NewGuid().ToString()}.jpeg";
            priveiwImage.SaveJpeg(Path.Combine(ImageRootPath, $"cover_{imageName}"), 80);

            string cover = Path.Combine(ImageRootPath, imageName);
            travelPlanDto.Cover.SaveJpeg(Path.Combine(ImageRootPath, cover), 80);

            List<TravelDayDAO> travelDays = new List<TravelDayDAO>();
            for (int i = 0; i < travelPlanDto.Days; i++)
            {
                var travelDay = new TravelDayDAO()
                {
                    TravelDate = travelPlanDto.StartDate.AddDays(i),
                    DayOrder = i+1,
                };
                travelDays.Add(travelDay);
            }  
            var travelPlan = new TravelPlanDAO(travelPlanDto.Title, travelPlanDto.Description, travelPlanDto.StartDate, travelPlanDto.Days, cover);
            var result = await _travelRepository.AddTravelPlanAsync(travelPlan);
            travelDays.ForEach(x=>x.TravelPlanId = result.Id);
            await _travelPlanDayRespository.AddTravelDayAsync(travelDays);
            TravelCardHandler.RenderTravelCard(new TravelPlanDTO(travelPlan.Id, travelPlan.Title, travelPlan.StartDate, travelPlan.Cover));
        }
    }
}
