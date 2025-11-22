using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanning.Components.TravelCardComponent;

namespace TravelPlanning.Views.Pages.Home
{
    [AddINotifyPropertyChangedInterface]
    public class HomeContext
    {
        public ObservableCollection<TravelCardViewModel> TravelCards { get; set; } = new ObservableCollection<TravelCardViewModel>()
        {

        };

        public void RenderTravelCards(List<TravelCardViewModel> newCards)
        {
            TravelCards.Clear();
            foreach (var card in newCards)
                TravelCards.Add(card);
        }
    }
}
