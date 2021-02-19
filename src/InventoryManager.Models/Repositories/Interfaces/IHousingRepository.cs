using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public interface IHousingRepository : IRepository
	{
		void AddHousing(Housing newHousing) =>
			DataContext.Housings.Add(newHousing);

		void RemoveHousing(Housing housingToRemove) =>
			DataContext.Housings.Remove(housingToRemove);

		void UpdateHousing(Housing housingToUpdate) =>
			DataContext.Housings.Update(housingToUpdate);

		IEnumerable<Housing> AllHousings =>
			DataContext.Housings.ToList();
	}
}
