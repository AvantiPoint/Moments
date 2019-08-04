using Prism.Behaviors;
using Xamarin.Forms;

namespace Moments.Behaviors
{
    public class MomentsBehaviorFactory : ExtendedPageBehaviorFactory
    {
        public MomentsBehaviorFactory(IPageBehaviorFactoryOptions options)
            : base(options)
        {
        }

        public override void ApplyCarouselPageBehaviors(CarouselPage page)
        {
            base.ApplyCarouselPageBehaviors(page);
            if (Device.RuntimePlatform != Device.Android)
            {
                NavigationPage.SetHasNavigationBar(page, false);
            }

            page.Behaviors.Add(new CarouselTitleBehavior());
            page.Title = page.CurrentPage.Title;
        }
    }
}
