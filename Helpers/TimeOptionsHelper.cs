using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanning.Helpers
{
    public static class TimeOptionsHelper
    {
        public static List<int> ArriveHours { get; } = Enumerable.Range(0, 13).ToList();
        public static List<int> ArriveMinutes { get; }
            = Enumerable.Range(0, 12)
                        .Select(x => x * 5)
                        .ToList();
        public static List<int> StayHours { get; } = Enumerable.Range(0, 24).ToList();
        public static List<int> StayMinutes { get; } = 
                        Enumerable.Range(0, 12)
                        .Select(x => x * 5)
                        .ToList();
        public static List<string> TimeOptions { get; } = new List<string> { "AM", "PM" };
    }
}
