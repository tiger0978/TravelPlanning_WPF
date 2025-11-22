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
using TravelPlanning.Components.TravelCardComponent;
using TravelPlanning.Contracts;
using TravelPlanning.Presenters;
using TravelPlanning.Respositories.Models.DAOs;
using TravelPlanning.Views.CreateTravels;

namespace TravelPlanning.Views.Pages.Home
{
    /// <summary>
    /// Home.xaml 的互動邏輯
    /// </summary>
    [NavigationItem("首頁", Wpf.Ui.Controls.SymbolRegular.Home24,0)]
    /// 
    public partial class Home : Page, IHomeView
    {
        public HomeContext Context { get; set; } = new HomeContext();

        public Home(IPresenterFactory presenterFactory)
        {
            InitializeComponent();
            this.DataContext = Context;
            var presenter = presenterFactory.CreatePresneter<IHomePresenter, IHomeView>(this);
            presenter.GetTravelCards();
        }


        public void RenderPage(List<TravelPlanDAO> plans)
        {
            var cards = plans.Select(x => new TravelCardViewModel(x.Title, x.StartDate, x.Cover)).ToList();
            Context.RenderTravelCards(cards);
        }


    }
}
