using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TravelPlanning.Models.Enums;

namespace TravelPlanning.Converters
{
    public class CommentStarCoverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(!float.TryParse(value.ToString(), out float rate))
            {
                return "";
            }
            string stars = "";
            for (int i = 1; i <= 5; i++)
            {
                if (rate >= i)
                {
                    stars += "★";
                }
                else if (rate >= i - 0.5f)
                {
                    stars += "⯪";

                }
                else
                {
                    stars += "☆";
                }
            }
            return stars;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
