using IoC_Container;
using System.Windows.Controls;
using TravelPlanning.Attributes;
using Wpf.Ui.Controls;

namespace TravelPlanning.Views.Pages.CreateTravels
{
    /// <summary>
    /// CreateTravel.xaml 的互動邏輯
    /// </summary>
    [NavigationItem("新增旅程", SymbolRegular.AddSquare24, 1)]
    public partial class CreateTravelPage : Page
    {

        public CreateTravelPage(IPresenterFactory presenterFactory)
        {
            InitializeComponent();
            this.DataContext = new CreateTravelContext(presenterFactory);
            //var presenter = presenterFactory.CreatePresneter<ICreateTravelPresenter, ICreateTravelView>(this);
            //var travelPlanDto = new TravelPlanDTO(Context.Title,Context.Description, Context.Days, Context.StartedDate, Context.Cover);
            //presenter.AddTravelPlan(travelPlanDto);
        }

    }
}
