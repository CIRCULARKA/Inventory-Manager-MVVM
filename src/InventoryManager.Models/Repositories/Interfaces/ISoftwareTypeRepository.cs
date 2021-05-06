using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface ISoftwareTypeRepository
	{
		void AddSoftwareType(SoftwareType typeToAdd);

		void RemoveSoftwareType(SoftwareType typeToRemove);

		void UpdateSoftwareType(SoftwareType typeToUpdate);

		SoftwareType FindSoftwareType(params object[] keys);

		IEnumerable<SoftwareType> AllSoftwareTypes { get; }
	}
}
