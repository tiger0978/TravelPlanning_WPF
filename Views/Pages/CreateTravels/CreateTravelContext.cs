using IoC_Container;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.Contracts;
using TravelPlanning.Utilties;
using IoC_Container.Attributes;

namespace TravelPlanning.Views.Pages.CreateTravels
{
    [Transient]
    [AddINotifyPropertyChangedInterface]
    public class CreateTravelContext : ICreateTravelPage
    {
        public string Title { get; set; } = "宜蘭三天兩夜";
        public int Days { get; set; } = 3;
        public string Description { get; set; } = "Desription of the travel plan";
        public DateTime StartedDate { get; set; } = DateTime.Now;
        public BitmapImage Cover { get; set; } = new BitmapImage(new Uri("pack://application:,,,/TravelPlanning;component/Resources/Image/Upload.png", UriKind.Absolute));
        public ICommand CreateTravelCommand { get; set; }
        public ICommand SelectImageCommand { get; set; }

        public CreateTravelContext(IPresenterFactory presenterFactory) 
        {
            var presenter = presenterFactory.CreatePresneter<ICreateTravelPresenter, ICreateTravelPage>(this);
            CreateTravelCommand = new RelayCommand(() =>
            {
                var travelPlanDto = new TravelPlanDTO(Title, Description, Days, StartedDate, Cover);
                presenter.AddTravelPlan(travelPlanDto);
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
