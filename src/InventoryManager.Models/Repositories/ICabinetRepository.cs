using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Models
{
	public interface ICabinetRepository : IRepository
	{
		void AddCabinet(Cabinet newCabinet) =>
			DataContext.Cabinets.Add(newCabinet);

		void RemoveCabinet(Cabinet cabinetToRemove) =>
			DataContext.Cabinets.Remove(cabinetToRemove);

		void UpdateCabinet(Cabinet cabinetToUpdate) =>
			DataContext.Cabinets.Update(cabinetToUpdate);

		IEnumerable<Cabinet> AllCabinets =>
			DataContext.
			Cabinets.
			Include(c => c.Housing).
			ToList();
	}
}
