using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace InventoryManager.Extensions
{
	public static class ListExtenstions
	{
		public static ObservableCollection<T> ToObservableCollection<T>(this List<T> list)
		{
			var result = new ObservableCollection<T>();
			foreach(var item in list)
				result.Add(item);
			return result;
		}
	}
}
