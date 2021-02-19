using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InventoryManager.Models
{
	public interface IUserRepository : IRepository
	{
		void AddUser(User newUser) =>
			DataContext.Users.Add(newUser);

		void RemoveUser(User userToRemove) =>
			DataContext.Users.Remove(userToRemove);

		void UpdateUser(User userToUpdate) =>
			DataContext.Users.Update(userToUpdate);

		IEnumerable<User> AllUsers =>
			DataContext.
			Users.
			Include(u => u.UserGroup).
			ToList();
	}
}
