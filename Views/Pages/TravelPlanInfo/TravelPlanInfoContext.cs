using CommunityToolkit.Mvvm.Messaging;
using GongSolutions.Wpf.DragDrop;
using GoogleMap.SDK.Contracts.GoogleAPI;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Response;
using IoC_Container;
using IoC_Container.Attributes;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TravelPlanning.Contracts;
using TravelPlanning.Messages.TravelPlanInfo;
using TravelPlanning.Utilties;
using TravelPlanning.Views.Pages.TravelPlanInfo.Models;

namespace TravelPlanning.Views.Pages.TravelPlanInfo
{
    [Transient]
    [AddINotifyPropertyChangedInterface]
    public class TravelPlanInfoContext : IDropTarget, ITravelPlanInfoView
    {
        private TravelInfoDayViewModel _selectedDay;
        private List<TravelPlanDayDTO> _travelDays;
        public ObservableCollection<TravelInfoDayViewModel> Days { get; set; }
        public ObservableCollection<TravelInfoPlaceViewModel> CurrentPlaces { get; set; }
        private int index = 0;
        public TravelInfoDayViewModel SelectedDay
        {
            get => _selectedDay;
            set
            {
                _selectedDay = value;
                CurrentPlaces = new ObservableCollection<TravelInfoPlaceViewModel>(value.TravelPlaces);
                ChangePage(value.Id);
            }
        }

        public bool IsEditingTime { get; set; }
        public ICommand DeleteDayCommand { get; set; }
        public ICommand DeleteSpotCommand { get; set; }
        public ICommand AddDayCommand { get; set; }
        public ICommand SelectItemCommand { get; set; }
        public ICommand UpdateTravelTimeCommand { get; set; }
        public ICommand CancelEditTimeCommand { get; set; }
        public ICommand SaveEditTimeCommand { get; set; }
        public ICommand ChangeTrafficWayCommand { get; set; }
        public Guid TravelPlanId { get; set; }
        public TravelPlanInfoContext(Guid travelPlanId, 
            IPresenterFactory presenterFactory)
        {
            var presenter = presenterFactory.CreatePresneter<ITravelPlanInfoPresenter, ITravelPlanInfoView>(this);
            presenter.GetTravelPlanInfo(travelPlanId);
            Days = new ObservableCollection<TravelInfoDayViewModel>();

            AddDayCommand = new RelayCommand(async () =>
            {
                await presenter.AddNewTravelDay(travelPlanId);
            });


            DeleteDayCommand = new RelayCommand<TravelInfoDayViewModel>((day) => 
            {
                Days.RemoveAt(Days.Count - 1);
            });
            SelectItemCommand = new RelayCommand<PlaceDetailResponse>(async (e) =>
            {
                var lastPlace = CurrentPlaces.LastOrDefault();
                var addPlaceDto = new AddTravelPlaceDTO
                {
                    TravelDayId = SelectedDay.Id,
                    Name = e.result.name,
                    PlaceId = e.result.place_id,
                    Duration = 30,
                    TravelTime = lastPlace == null ? DateTime.Parse(SelectedDay.TravelDate) : DateTime.Parse($"{SelectedDay.TravelDate} {lastPlace.TravelTime}").AddMinutes(30)
                };
                await presenter.AddNewPlace(addPlaceDto);
            });
            DeleteSpotCommand = new RelayCommand<TravelInfoPlaceViewModel>(async x =>
            {
                var deletePlaceDto = new DeleteTravelPlaceDTO
                {
                    TravelDayId = SelectedDay.Id,
                    TravelPlaceId = x.Id
                };
                await presenter.DeletePlace(deletePlaceDto);
            });
            CancelEditTimeCommand = new RelayCommand<TravelInfoPlaceViewModel>((place) =>
            {
                place.IsEditingTime = false;
            });
            SaveEditTimeCommand = new RelayCommand<TravelInfoPlaceViewModel>(async place =>
            {
                place.IsEditingTime = false;
                var placeDto = new TravelPlaceDTO
                {
                    Id = place.Id,
                    Duration = place.Duration,
                    TravelTime = place.TravelDate,
                    TravelDayId = place.TravelDayId,
                    HasArrivedTime = place.HasArrivedTime
                };
                await presenter.UpdateTravelTime(placeDto);
            });
            ChangeTrafficWayCommand = new RelayCommand<TravelInfoPlaceViewModel>(async place =>
            {
                var placeDto = new TravelPlaceDTO
                {
                    Id = place.Id,
                    TravelDayId = place.TravelDayId,
                    TrafficType = place.TrafficMode
                };
                await presenter.UpdateTravelWay(placeDto);
            });

        }

        // Drag behavior
        public void DragOver(IDropInfo dropInfo)
        {
            //dropInfo.Effects = System.Windows.DragDropEffects.Move;
        }

        public void Drop(IDropInfo dropInfo)
        {
            //var source = dropInfo.Data as TravelInfoDayViewModel;
            //var target = dropInfo.TargetItem as TravelInfoDayViewModel;
            //if (source == null || target == null || source == target) return;
            //int oldIndex = Days.IndexOf(source);
            //int newIndex = Days.IndexOf(target);
            //Days.Move(oldIndex, newIndex);
        }

        public ICommand ScrollLeftCommand => new RelayCommand(() =>
            ScrollRequested?.Invoke(-140)); // 每次捲動一格

        public ICommand ScrollRightCommand => new RelayCommand(() => {

            ScrollRequested?.Invoke(140);
            //SelectedDay
            SelectedDay = Days[++index];

        });


        public event Action<double> ScrollRequested;

        public void DropHint(IDropHintInfo dropHintInfo)
        {
            throw new NotImplementedException();
        }

        public void DragEnter(IDropInfo dropInfo)
        {
            throw new NotImplementedException();
        }

        public void DragLeave(IDropInfo dropInfo)
        {
            
        }
        public void OnTravelPlanInfoResponse(TravelPlanDTO travelPlanDTO)
        {
            var days = travelPlanDTO.TravelDays.Select(x => 
            {
                return new TravelInfoDayViewModel
                {
                    Id = x.Id,
                    DayName = x.DayName,
                    TravelDate = x.TravelDate.ToString("yyyy-MM-dd"),
                    TravelPlaces = x.TravelPlaces.Select(y =>
                    {
                        return new TravelInfoPlaceViewModel
                        {
                            Id = y.Id,
                            TravelDayId = y.TravelDayId,
                            Duration = y.Duration,
                            Name = y.Name,
                            TravelTime = y.TravelTime.ToString("HH:mm"),
                            TravelDate = x.TravelDate,
                            Cover = y.Cover,
                            HasArrivedTime = y.HasArrivedTime,
                            TrafficMode = y.TrafficType,
                            PlaceId = y.PlaceId,
                            PreviousLeavingTime = y.LastPlaceLeavingTime.HasValue ? y.LastPlaceLeavingTime.Value.ToString("HH:mm") : "",
                            DisplayDrivingDuration = $"約{y.TrafficDuration}分"
                        };
                    }).ToList()
                };
            }).ToList();
            Days = new ObservableCollection<TravelInfoDayViewModel>(days);
            _travelDays = travelPlanDTO.TravelDays;
            SelectedDay = Days.FirstOrDefault();
            CurrentPlaces = new ObservableCollection<TravelInfoPlaceViewModel>(days[0].TravelPlaces);
            WeakReferenceMessenger.Default.Send(new RouteRenderMessage(travelPlanDTO.TravelDays[0].TravelPlaces));
        }
        public void OnAddTravelPlaceResponse(List<TravelPlaceDTO> travelPlacesDTO)
        {
            RenderCurrentDayPlacesList(travelPlacesDTO);
            WeakReferenceMessenger.Default.Send(new RouteRenderMessage(travelPlacesDTO));

        }
        public void OnDeleteTravelPlaceResponse(List<TravelPlaceDTO> travelPlacesDTO)
        {
            RenderCurrentDayPlacesList(travelPlacesDTO);
            WeakReferenceMessenger.Default.Send(new RouteRenderMessage(travelPlacesDTO));
        }
        public void OnEditTravelPlaceTimeResponse(List<TravelPlaceDTO> travelPlacesDTO)
        {
            RenderCurrentDayPlacesList(travelPlacesDTO);
            WeakReferenceMessenger.Default.Send(new RouteRenderMessage(travelPlacesDTO));
        }

        public void OnAddNewTravelDay(TravelPlanDayDTO travelPlanDayDTO)
        {
            var travelDayViewModel = new TravelInfoDayViewModel
            {
                Id = travelPlanDayDTO.Id,
                DayName = travelPlanDayDTO.DayName,
                TravelDate = travelPlanDayDTO.TravelDate.ToString("yyyy-MM-dd"),
                TravelPlaces = new List<TravelInfoPlaceViewModel>()
            };
            Days.Add(travelDayViewModel);
        }

        private void ChangePage(Guid dayId) 
        {
            var day = _travelDays.FirstOrDefault(x => x.Id == dayId);
            WeakReferenceMessenger.Default.Send(new RouteRenderMessage(day.TravelPlaces));
        }


        private void RenderCurrentDayPlacesList(List<TravelPlaceDTO> travelPlacesDTO) 
        {
            var viewModels = travelPlacesDTO.Select(y => new TravelInfoPlaceViewModel
            {
                Id = y.Id,
                Duration = y.Duration,
                Cover = y.Cover,
                Name = y.Name,
                TravelDayId = y.TravelDayId,
                TravelTime = y.TravelTime.ToString("HH:mm"),
                TravelDate = y.TravelTime,
                TrafficMode = y.TrafficType,
                HasArrivedTime = y.HasArrivedTime,
                PreviousLeavingTime = y.LastPlaceLeavingTime.HasValue? y.LastPlaceLeavingTime.Value.ToString("HH:mm") : "",
                DisplayDrivingDuration = $"約{y.TrafficDuration}分"
            });
            CurrentPlaces = new ObservableCollection<TravelInfoPlaceViewModel>(viewModels);
            var targetDay = Days.FirstOrDefault(day => day.Id == SelectedDay.Id);
            if (targetDay != null)
            {
                targetDay.TravelPlaces = viewModels.ToList();
            }
            var targetDayDto = _travelDays.FirstOrDefault(day => day.Id == SelectedDay.Id);
            if (targetDayDto != null)
            {
                targetDayDto.TravelPlaces = travelPlacesDTO;
            }
        }


    }
}
