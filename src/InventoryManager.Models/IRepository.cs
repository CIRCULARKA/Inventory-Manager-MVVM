using InventoryManager.Data;

namespace InventoryManager.Models
{
	public interface IRepository<T> where T : class
	{
		InventoryManagerDbContext DataContext { get; }
		void Add(T entity) => DataContext.Add<T>(entity);

		void Remove(T entity) => DataContext.Remove<T>(entity);

		void Update(T entity) => DataContext.Update<T>(entity);

		void Find(T entity) => DataContext.Find<T>(entity);

		void SaveChanges() => DataContext.SaveChanges();
	}
}
