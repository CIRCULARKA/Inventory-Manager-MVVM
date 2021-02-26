using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface ICabinetRepository : IRepository
	{
		void AddCabinet(Cabinet newCabinet);

		void RemoveCabinet(Cabinet cabinetToRemove);

		void UpdateCabinet(Cabinet cabinetToUpdate);

		Cabinet FindCabinet(params object[] keys);

		IEnumerable<Cabinet> AllCabinets { get; }
	}
}
