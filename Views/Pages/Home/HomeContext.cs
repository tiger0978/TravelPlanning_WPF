using IoC_Container;
using IoC_Container.Attributes;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelPlanning.Components.TravelCard;
using TravelPlanning.Contracts;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.EventHandlers;
using TravelPlanning.Utilties;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace TravelPlanning.Views.Pages.Home
{
    [Transient]
    [AddINotifyPropertyChangedInterface]
    public class HomeContext : IHomePage
    {
        public ObservableCollection<TravelCardContext> TravelCards { get; set; } = new ObservableCollection<TravelCardContext>() { };

        public ICommand DeleteCommand { get; set; }
        private IContentDialogService _dialogService;
        private IHomePresenter _presenter;


        public HomeContext(IPresenterFactory presenterFactory, IContentDialogService dialogService) 
        {
            var presenter = presenterFactory.CreatePresneter<IHomePresenter, IHomePage>(this);
            _presenter = presenter;
            presenter.GetTravelCards();
            TravelCardHandler.ReceivedTravelCard += TravelCardHandler_ReceivedTravelCard;
            _dialogService = dialogService;
            DeleteCommand = new RelayCommand(async(card) =>
            {
                await DeleteCard(card);
            });

        }

        public void RenderPage(List<TravelPlanDTO> plans)
        {
            var cards = plans.Select(x => new TravelCardContext(x.Id, x.Title, x.StartDate, x.Cover)).ToList();
            TravelCards.Clear();
            foreach (var card in cards)
            {
                TravelCards.Add(card);
            }
        }
        private async Task DeleteCard(object obj) 
        {
            if (obj is TravelCardContext card)
            {
                var dialog = await _dialogService.ShowAsync(
                            new ContentDialog
                            {
                                Title = "確認刪除",
                                Content = $"確定要刪除「{card.Title}」嗎？",
                                PrimaryButtonText = "刪除",
                                CloseButtonText = "取消",
                                PrimaryButtonAppearance = ControlAppearance.Danger
                            },CancellationToken.None
                        );
                if (dialog == ContentDialogResult.Primary)
                {
                    TravelCards.Remove(card);
                    await _presenter.DeleteTravelCard(card.Id);
                }
            }
        }

        private void TravelCardHandler_ReceivedTravelCard(object sender, TravelPlanDTO e)
        {
            var card = new TravelCardContext(e.Id, e.Title, e.StartDate, e.Cover);
            TravelCards.Add(card);
        }

    }
}
