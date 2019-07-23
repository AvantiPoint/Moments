using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Moments
{
	public class DrawMomentViewModel : BaseViewModel
	{
		Color penColor;
		int selectedIndex;

		public DrawMomentViewModel (byte[] image)
		{
			Image = image;
			PenColor = Color.Yellow;
			SelectedIndex = 2;
		}

		public byte[] Image { get; set; }

		public Color PenColor
		{
			get { return penColor; }
			set { penColor = value; OnPropertyChanged ("PenColor"); } 
		}

		public int SelectedIndex
		{
			get { return selectedIndex; }
			set { selectedIndex = value; OnPropertyChanged ("SelectedIndex"); }
		}

		public int DisplayTime
		{
			get { return SelectedIndex + 3; }
		}
	}
}

