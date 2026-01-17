using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace TravelPlanning.Models
{
    public class GridLengthAnimation : AnimationTimeline
    {
        public IEasingFunction EasingFunction
        {
            get => (IEasingFunction)GetValue(EasingFunctionProperty);
            set => SetValue(EasingFunctionProperty, value);
        }

        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register(nameof(EasingFunction),
                typeof(IEasingFunction),
                typeof(GridLengthAnimation));

        public override Type TargetPropertyType => typeof(GridLength);

        public GridLength From
        {
            get => (GridLength)GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }

        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register(nameof(From), typeof(GridLength), typeof(GridLengthAnimation));

        public GridLength To
        {
            get => (GridLength)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register(nameof(To), typeof(GridLength), typeof(GridLengthAnimation));

        protected override Freezable CreateInstanceCore()
            => new GridLengthAnimation();

        public override object GetCurrentValue(
            object defaultOriginValue,
            object defaultDestinationValue,
            AnimationClock animationClock)
        {
            double fromVal = From.Value;
            double toVal = To.Value;

            double progress = animationClock.CurrentProgress ?? 0;

            if (EasingFunction != null)
                progress = EasingFunction.Ease(progress);

            double current = fromVal + (toVal - fromVal) * progress;

            return new GridLength(current, GridUnitType.Pixel);
        }
    }
}
