using IoC_Container;
using IoC_Container.Attributes;
using PropertyChanged;
using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TravelPlanning.Contracts;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.Utilties;

namespace TravelPlanning.Views.Pages.CreateTravels
{
    [Transient]
    [AddINotifyPropertyChangedInterface]
    public class CreateTravelContext : ICreateTravelPage
    {
        public string Title { get; set; } = "宜蘭三天兩夜";
        public string Days { get; set; } = "3";
        public string Description { get; set; } = "Desription of the travel plan";
        public DateTime StartedDate { get; set; } = DateTime.Now;
        public BitmapImage Cover { get; set; } = new BitmapImage(new Uri("pack://application:,,,/TravelPlanning;component/Resources/Image/Upload.png", UriKind.Absolute));
        public ICommand CreateTravelCommand { get; set; }
        public ICommand SelectImageCommand { get; set; }

        public CreateTravelContext(IPresenterFactory presenterFactory)
        {
            var presenter = presenterFactory.CreatePresneter<ICreateTravelPresenter, ICreateTravelPage>(this);
            CreateTravelCommand = new RelayCommand(async () =>
            {
                var travelPlanDto = new TravelPlanDTO(Title, Description, int.Parse(Days), StartedDate, Cover);
                await presenter.AddTravelPlanAsync(travelPlanDto);
            });

            SelectImageCommand = new RelayCommand(() =>
            {
                SelectCover();
            });
        }
        private void SelectCover()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Images|*.png;*.jpg;*.jpeg;*.bmp";

            if (dialog.ShowDialog() == true)
            {
                BitmapImage img = new BitmapImage(new Uri(dialog.FileName));
                Cover = img;
            }
        }
    }
}
