using System.Collections.Generic;

namespace InventoryManager.Models
{
	public interface IUserRepository
	{
		void AddUser(User newUser);

		void RemoveUser(User userToRemove);

		void UpdateUser(User userToUpdate);

		IEnumerable<User> AllUsers { get; }
	}
}
