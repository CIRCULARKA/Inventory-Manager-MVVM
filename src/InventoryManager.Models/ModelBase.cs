using InventoryManager.Data;
using System.Collections.Generic;

namespace InventoryManager.Models
{
	public abstract class ModelBase<T> where T: class
	{
		public virtual void Add(T entity) => DataContext.Add<T>(entity);

		public T Find(params object[] keys) => DataContext.Find<T>(keys);

		public virtual void Remove(T entity) => DataContext.Remove<T>(entity);

		public void Update(T entity) => DataContext.Update<T>(entity);

		/// <summary>
		/// This method saves all changes with all models whatever model it called from
		/// </summary>
		public void SaveChanges() => DataContext.SaveChanges(true);

		public abstract List<T> All();

		public static InventoryManagerDbContext DataContext { get; } = new InventoryManagerDbContext();
	}
}
