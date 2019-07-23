using System;
using System.Net;
using Xamarin.Forms;

namespace Moments
{
	public class ViewMomentViewModel : BaseViewModel
	{
		string image;
		TimeSpan momentViewTime;

		public ViewMomentViewModel (string momentUrl, TimeSpan viewTime)
		{
			image = momentUrl;
			momentViewTime = viewTime;
		}

		public string Image
		{
			get { return image; }
		}

		public TimeSpan MomentViewTime
		{
			get { return momentViewTime; }
		}
	}
}