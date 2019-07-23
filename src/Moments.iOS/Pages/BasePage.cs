using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.Linq;
using Moments.Views;

[assembly:ExportRenderer(typeof(Moments.Views.BasePage), typeof(Moments.iOS.BasePageRenderer))]
namespace Moments.iOS
{
	public class BasePageRenderer : PageRenderer
	{
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear (animated);

			var page = this.Element as BasePage;
			page.LeftToolbarItems.CollectionChanged += (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) => {
				UpdateView ();
			};
		}

		private void UpdateView ()
		{
			var leftToolbarItems = (this.Element as BasePage).LeftToolbarItems;
			if (leftToolbarItems.Count != 0) {
				NavigationController.TopViewController.NavigationItem.LeftBarButtonItems = GetBarButtonItems (leftToolbarItems);
			} else {
				NavigationController.TopViewController.NavigationItem.LeftBarButtonItems = new UIBarButtonItem[] { };
			}
		}

		private UIBarButtonItem[] GetBarButtonItems (IEnumerable<ToolbarItem> items) 
		{
			var leftBarButtonItem = new UIBarButtonItem ();
			foreach (var item in items) {
				if (item.Priority == 1) {
					leftBarButtonItem =  new UIBarButtonItem (UIImage.FromFile (item.Icon), UIBarButtonItemStyle.Plain, ((object sender, EventArgs e) => {
						item.Command.Execute (null);
					}));
				}
			}

			return new UIBarButtonItem[] { leftBarButtonItem };
		}
	}
}