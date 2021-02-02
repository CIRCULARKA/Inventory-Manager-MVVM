using InventoryManager.Data;

namespace InventoryManager.Models
{
	public abstract class ModelBase<T> where T: class
	{
		protected ModelBase()
		{
			DataContext = new InventoryManagerDbContext();
		}

		public void Add(T entity) => DataContext.Add<T>(entity);

		public void Remove(T entity) => DataContext.Remove<T>(entity);

		public void Update(T entity) => DataContext.Update<T>(entity);

		public void SaveChanges() => DataContext.SaveChanges();

		public InventoryManagerDbContext DataContext { get; }
	}
}
