using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface ICabinetRepository
	{
		void AddCabinet(Cabinet newCabinet);

		void RemoveCabinet(Cabinet cabinetToRemove);

		void UpdateCabinet(Cabinet cabinetToUpdate);

		IEnumerable<Cabinet> AllCabinets { get; }
	}
}
