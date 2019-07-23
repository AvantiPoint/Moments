using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Moments
{
	public class BaseViewModel : INotifyPropertyChanged, INotifyPropertyChanging
	{
		private string title = string.Empty;
		private string subTitle = string.Empty;
		private string icon = null;
		private bool isBusy;

		public const string TitlePropertyName = "Title";
		public const string SubtitlePropertyName = "Subtitle";
		public const string IconPropertyName = "Icon";
		public const string IsBusyPropertyName = "IsBusy";

		public event PropertyChangingEventHandler PropertyChanging;
		public event PropertyChangedEventHandler PropertyChanged;

		public BaseViewModel ()
		{

		}

		public string Title
		{
			get { return title; }
			set { SetProperty (ref title, value, TitlePropertyName);}
		}
		
		public string Subtitle
		{
			get { return subTitle; }
			set { SetProperty (ref subTitle, value, SubtitlePropertyName);}
		}

		public string Icon
		{
			get { return icon; }
			set { SetProperty (ref icon, value, IconPropertyName);}
		}

		public bool IsBusy 
		{
			get { return isBusy; }
			set { SetProperty (ref isBusy, value, IsBusyPropertyName);}
		}
			
		protected void SetProperty<T> (ref T backingStore, T value, string propertyName, Action onChanged = null, Action<T> onChanging = null) 
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value)) 
				return;

			if (onChanging != null) 
				onChanging(value);

			OnPropertyChanging(propertyName);

			backingStore = value;

			if (onChanged != null) 
				onChanged();

			OnPropertyChanged(propertyName);
		}

		public void OnPropertyChanging(string propertyName)
		{
			if (PropertyChanging == null)
				return;

			PropertyChanging (this, new PropertyChangingEventArgs (propertyName));
		}

		public void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
		}
	}
}