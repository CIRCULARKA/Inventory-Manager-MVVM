using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace InventoryManager.Models
{
	public interface IUserGroupRepository : IRepository
	{
		void AddUserGroup(UserGroup newGroup) =>
			DataContext.UserGroups.Add(newGroup);

		void RemoveUserGroup(UserGroup groupToRemove) =>
			DataContext.UserGroups.Remove(groupToRemove);

		void UpdateUserGroup(UserGroup groupToUpdate) =>
			DataContext.UserGroups.Update(groupToUpdate);

		IEnumerable<UserGroup> AllUserGroups =>
			DataContext.UserGroups.ToList();
	}
}
