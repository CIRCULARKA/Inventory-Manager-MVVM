using System.Linq;
using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface ISoftwareRelatedRepository :
		ISoftwareConfigurationRepository, ISoftwareRepository,
		ISoftwareTypeRepository { }
}
