using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

// Source: https://stackoverflow.com/questions/2184169/add-elements-from-ilist-to-observablecollection
namespace Moments
{
	public static class ObservableExtensions
	{
		public static void AddRange<T> (this ObservableCollection<T> collection, IEnumerable<T> items)
		{
			items.ToList ().ForEach (collection.Add);
		}    
	}
}

