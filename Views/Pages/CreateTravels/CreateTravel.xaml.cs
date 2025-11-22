using IoC_Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TravelPlanning.Attributes;
using TravelPlanning.Contracts;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.Views.CreateTravels;
using Wpf.Ui.Controls;

namespace TravelPlanning.Views.Pages
{
    /// <summary>
    /// CreateTravel.xaml 的互動邏輯
    /// </summary>
    [NavigationItem("新增旅程", SymbolRegular.AddSquare24, 1)]
    public partial class CreateTravel : Page, ICreateTravelView
    {

        public CreateTravel(IPresenterFactory presenterFactory)
        {
            InitializeComponent();
            this.DataContext = new CreateTravelContext(presenterFactory);
            //var presenter = presenterFactory.CreatePresneter<ICreateTravelPresenter, ICreateTravelView>(this);
            //var travelPlanDto = new TravelPlanDTO(Context.Title,Context.Description, Context.Days, Context.StartedDate, Context.Cover);
            //presenter.AddTravelPlan(travelPlanDto);
        }

    }
}
