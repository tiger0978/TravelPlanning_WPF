using CommunityToolkit.Mvvm.Messaging;
using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using IoC_Container;
using IoC_Container.Attributes;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TravelPlanning.Components.MapPanels.SearchPanel;
using TravelPlanning.Contracts.DTOs;
using TravelPlanning.Messages;
using TravelPlanning.Models;
using TravelPlanning.Utilties;
using Wpf.Ui.Controls;

namespace TravelPlanning.Components.MapPanels
{
    [AddINotifyPropertyChangedInterface]
    [Transient]
    public class MapPanelContext
    {
        public ICommand SelectItemCommand { get; set; }
        public ICommand ChangePageCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public List<NavigationPageItem> PageItems { get; set; }
        public Visibility ToggleButtonVisibility { get; set; } = Visibility.Collapsed;
        public NavigationProvider NavigationProvider { get; set; }

        public MapPanelContext(NavigationProvider navigationProvider)
        {
            NavigationProvider = navigationProvider;
            PageItems = NavigationProvider.GetPages<NavigationPageItem>("TravelPlanning.Components.MapPanels");
         
            SelectItemCommand = new RelayCommand<PlaceDetailResponse>((e) =>
            {
                WeakReferenceMessenger.Default.Send(new PlaceSelectedMessage(e));
                var userControl = NavigationProvider.Navigate(typeof(SearchPanelComponent), null);
                var panelContext = ((SearchPanelComponent)userControl).Context;
                panelContext.RenderModel(e);
                ToggleButtonVisibility = Visibility.Visible;
            });

            ClearCommand = new RelayCommand(() =>
            {
                navigationProvider.ClearControl();
                //view.Text = "";
                WeakReferenceMessenger.Default.Send(new InitialMapOverlayMessage());
                ToggleButtonVisibility = Visibility.Collapsed;
            });
            ChangePageCommand = new RelayCommand<Type>(pageType =>
            {
                navigationProvider.Navigate(pageType, null);
                WeakReferenceMessenger.Default.Send(new InitialMapOverlayMessage());
                ToggleButtonVisibility = Visibility.Visible;
            });

        }
    }
}
