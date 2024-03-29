using InventoryManager.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace InventoryManager.Models
{
	public class DefaultUserRelatedRepository : IUserRelatedRepository
	{
		BaseDbContext DataContext { get; } = new DefaultDbContext();

		public void AddUser(User newUser) =>
			DataContext.Users.Add(newUser);

		public void RemoveUser(User userToRemove) =>
			DataContext.Users.Remove(userToRemove);

		public void UpdateUser(User userToUpdate) =>
			DataContext.Users.Update(userToUpdate);

		public User FindUser(string login, string include = null)
		{
			var set = DataContext.Users;
			IQueryable<User> query = set;
			if (include != null)
				foreach (var property in include.Split(new char[] { ',', ' ', '.' }))
					query = set.Include(property);

			return query.FirstOrDefault(u => u.Login == login);
		}

		public IEnumerable<User> AllUsers =>
			DataContext.
			Users.
			Include(u => u.UserGroup).
			ToList();

		public void AddUserGroup(UserGroup newGroup) =>
			DataContext.UserGroups.Add(newGroup);

		public void RemoveUserGroup(UserGroup groupToRemove) =>
			DataContext.UserGroups.Remove(groupToRemove);

		public void UpdateUserGroup(UserGroup groupToUpdate) =>
			DataContext.UserGroups.Update(groupToUpdate);

		public IEnumerable<UserGroup> AllUserGroups =>
			DataContext.UserGroups.ToList();

		public void SaveChanges() => DataContext.SaveChanges();
	}
}
