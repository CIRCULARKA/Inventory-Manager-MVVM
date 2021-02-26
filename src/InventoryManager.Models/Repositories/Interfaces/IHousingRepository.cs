using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface IHousingRepository : IRepository
	{
		void AddHousing(Housing newHousing);

		void RemoveHousing(Housing housingToRemove);

		void UpdateHousing(Housing housingToUpdate);

		IEnumerable<Housing> AllHousings { get; }
	}
}
