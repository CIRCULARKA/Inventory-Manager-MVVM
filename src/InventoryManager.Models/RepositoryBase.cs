using InventoryManager.Data;
using System.Collections.Generic;

namespace InventoryManager.Models
{
	public abstract class RepositoryBase<T> where T : class
	{
		protected InventoryManagerDbContext DataContext { get; }

		protected RepositoryBase() => DataContext = new InventoryManagerDbContext();

		public void Add(T entity) => DataContext.Add<T>(entity);

		public void Remove(T entity) => DataContext.Remove<T>(entity);

		public void Update(T entity) => DataContext.Update<T>(entity);

		public void Find(T entity) => DataContext.Find<T>(entity);

		public void SaveChanges() => DataContext.SaveChanges();

		public abstract IEnumerable<T> All();
	}
}
