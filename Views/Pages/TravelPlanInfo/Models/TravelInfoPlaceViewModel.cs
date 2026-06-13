using GoogleMap.SDK.Contracts.Commons.Enums;
using PropertyChanged;
using System;
using System.Windows.Media.Imaging;

namespace TravelPlanning.Views.Pages.TravelPlanInfo.Models
{
    [AddINotifyPropertyChangedInterface]
    public class TravelInfoPlaceViewModel
    {
        public Guid Id { get; set; }
        public Guid TravelDayId { get; set; }
        public string PlaceId { get; set; }
        public string Name { get; set; }
        public BitmapImage Cover { get; set; }
        public string TravelTime { get; set; }
        public int Duration { get; set; }
        public int ArrivalHour { get; set; }
        public int ArrivalMinute { get; set; }
        public int StayHour { get; set; }
        public int StayMinute { get; set; }
        public string ArrivalAmPm { get; set; }
        public bool IsEditingTime { get; set; }
        public DateTime TravelDate { get; set; }
        public bool HasArrivedTime { get; set; } 
        public string DisplayDrivingDuration { get; set; }
        public string PreviousLeavingTime { get; set; }
        public TrafficMode TrafficMode { get; set; }

        private bool _isUpdating;

        // ==========================================
        // TravelTime -> ArrivalXXX
        // ==========================================
        private void OnTravelTimeChanged()
        {
            if (_isUpdating) return;
            if (string.IsNullOrWhiteSpace(TravelTime)) return;

            if (DateTime.TryParse(TravelTime, out var parsed))
            {
                _isUpdating = true;

                ArrivalAmPm = parsed.Hour < 12 ? "AM" : "PM";

                int hour12 = parsed.Hour % 12;
                ArrivalHour = hour12 == 0 ? 12 : hour12;

                ArrivalMinute = parsed.Minute;

                _isUpdating = false;
            }
        }

        // ==========================================
        // ArrivalXXX -> TravelTime
        // ==========================================
        private void OnArrivalHourChanged() => UpdateTravelTime();

        private void OnArrivalMinuteChanged() => UpdateTravelTime();

        private void OnArrivalAmPmChanged() => UpdateTravelTime();

        private void UpdateTravelTime()
        {
            if (_isUpdating) return;

            _isUpdating = true;

            int hour24 = ArrivalHour;

            if (ArrivalAmPm == "PM" && hour24 != 12)
                hour24 += 12;

            if (ArrivalAmPm == "AM" && hour24 == 12)
                hour24 = 0;

            TravelDate = new DateTime(
                TravelDate.Year,
                TravelDate.Month,
                TravelDate.Day,
                hour24,
                ArrivalMinute,
                0);

            TravelTime = TravelDate.ToString("HH:mm");
            _isUpdating = false;
        }

        // ==========================================
        // Duration -> StayHour / StayMinute
        // ==========================================
        private void OnDurationChanged()
        {
            if (_isUpdating) return;

            _isUpdating = true;

            StayHour = Duration / 60;
            StayMinute = Duration % 60;

            _isUpdating = false;
        }

        // ==========================================
        // StayHour / StayMinute -> Duration
        // ==========================================
        private void OnStayHourChanged() => UpdateDuration();

        private void OnStayMinuteChanged() => UpdateDuration();

        private void UpdateDuration()
        {
            if (_isUpdating) return;

            _isUpdating = true;

            Duration = (StayHour * 60) + StayMinute;

            _isUpdating = false;
        }
    }
}
