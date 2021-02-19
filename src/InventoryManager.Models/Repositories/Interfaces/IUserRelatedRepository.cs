using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public interface IUserRelatedRepository
		: IUserRepository, IUserGroupRepository { }
}
