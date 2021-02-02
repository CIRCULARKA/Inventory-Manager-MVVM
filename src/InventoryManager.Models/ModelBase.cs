using InventoryManager.Data;
using System.Collections.Generic;

namespace InventoryManager.Models
{
	public abstract class ModelBase<T> where T: class
	{
		protected ModelBase()
		{
			DataContext = new InventoryManagerDbContext();
		}

		public void Add(T entity) => DataContext.Add<T>(entity);

		public T Find(params object[] keys) => DataContext.Find<T>(keys);

		public void Remove(T entity) => DataContext.Remove<T>(entity);

		public void Update(T entity) => DataContext.Update<T>(entity);

		/// <summary>
		/// This method saves all changes with all models whatever model it called from
		/// </summary>
		public void SaveChanges() => DataContext.SaveChanges();

		public abstract List<T> All();

		public InventoryManagerDbContext DataContext { get; }
	}
}
