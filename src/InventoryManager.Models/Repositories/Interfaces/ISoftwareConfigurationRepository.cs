using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface ISoftwareConfigurationRepository
	{
		void AddSoftwareConfiguration(SoftwareConfiguration configToAdd);

		void RemoveSoftwareConfiguration(SoftwareConfiguration configToRemove);

		void UpdateSoftwareConfiguration(SoftwareConfiguration configToUpdate);

		SoftwareConfiguration FindSoftwareConfiguration(params object[] keys);

		IEnumerable<SoftwareConfiguration> AllSoftwareConfiguration { get; }
	}
}
