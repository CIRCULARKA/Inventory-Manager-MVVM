using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Models
{
	public class UserRepository : RepositoryBase<User>
	{
		public override IEnumerable<User> All =>
			DataContext.Users.ToList();

		public IEnumerable<UserGroup> AllUserGroups =>
			DataContext.UserGroups.ToList();
	}
}
