using System;
using Prism.Behaviors;
using Xamarin.Forms;

namespace Moments.Behaviors
{
    public class CarouselTitleBehavior : BehaviorBase<CarouselPage>
    {
        protected override void OnAttachedTo(CarouselPage bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.CurrentPageChanged += OnCurrentPageChanged;
        }

        protected override void OnDetachingFrom(CarouselPage bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.CurrentPageChanged -= OnCurrentPageChanged;
        }

        private void OnCurrentPageChanged(object sender, EventArgs e)
        {
            AssociatedObject.Title = AssociatedObject.CurrentPage.Title;
        }
    }
}
