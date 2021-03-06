using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace InventoryManager.Extensions
{
	public static class IEnumerableEstensions
	{
		public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
		{
			var result = new ObservableCollection<T>();
			foreach(var item in list)
				result.Add(item);
			return result;
		}
	}
}
