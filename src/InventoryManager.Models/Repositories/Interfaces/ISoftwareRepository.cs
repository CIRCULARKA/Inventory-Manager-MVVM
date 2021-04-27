using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface ISoftwareRepository
	{
		void AddSoftware(Software softwareToAdd);

		void RemoveSoftware(Software softwareToRemove);

		void UpdateSoftware(Software softwareToUpdate);

		Software FindSoftware(params object[] keys);

		IEnumerable<Software> AllSoftware { get; }
	}
}
