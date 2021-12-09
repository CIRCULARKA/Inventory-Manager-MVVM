using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface ICabinetRepository : IRepository
	{
		void AddCabinet(Cabinet newCabinet);

		void RemoveCabinet(Cabinet cabinetToRemove);

		void UpdateCabinet(Cabinet cabinetToUpdate);

		Cabinet FindCabinet(params object[] keys);

		Cabinet FindCabinetByName(string cabName, string housingName);

		IEnumerable<Cabinet> AllCabinets { get; }
	}
}
