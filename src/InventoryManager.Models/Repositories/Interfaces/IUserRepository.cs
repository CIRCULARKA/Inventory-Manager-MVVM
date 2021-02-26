using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InventoryManager.Models
{
	public interface IUserRepository : IRepository
	{
		void AddUser(User newUser);

		void RemoveUser(User userToRemove);

		void UpdateUser(User userToUpdate);

		User FindUser(params object[] keys);

		IEnumerable<User> AllUsers { get; }
	}
}
