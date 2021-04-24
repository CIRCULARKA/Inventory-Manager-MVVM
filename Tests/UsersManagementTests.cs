using Xunit;
using Moq;
using InventoryManager.Models;
using InventoryManager.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManager.Tests
{
	public class UsersManagementTests
	{
		private IEnumerable<User> BuildTestUsers() =>
			new User[]
			{
				new User
				{
					Login = "user1",
					Password = "user1password",
					FirstName = "TestFirstName1",
					LastName = "TestLastName1",
					MiddleName = "TestMiddleName1",
					UserGroupID = -1
				},
				new User
				{
					Login = "user2",
					Password = "user2password",
					FirstName = "TestFirstName2",
					LastName = "TestLastName2",
					MiddleName = "TestMiddleName2",
					UserGroupID = -1
				}
			};

		[Fact]
		public void AreUsersCanBeDisplayed()
		{
			// Arrange
			var mock = new Mock<IUserRelatedRepository>();
			var testUsers = BuildTestUsers();
			mock.Setup(r => r.AllUsers).Returns(testUsers);

			var vm1 = new UserViewModel(mock.Object, null);

			// Act
			var result = vm1.UsersToShow;

			// Assert
			Assert.Equal(testUsers, vm1.UsersToShow);
		}

		[Fact]
		public void IsAddedUserCanBeDisplayed()
		{
			// Arrange
			var mock = new Mock<IUserRelatedRepository>();

			var addUserVM = new AddUserViewModel(mock.Object);
			var mainUserVM = new UserViewModel(mock.Object, null);
			var userToAdd = new User
			{
				Login = "testAddedUserLogin"
			};

			addUserVM.InputtedLogin = userToAdd.Login;
			addUserVM.SelectedUserGroup = new UserGroup { ID = 1 };

			// Act
			addUserVM.AddUserCommand.Execute(null);

			// Assert
			Assert.NotEmpty(mainUserVM.UsersToShow.Where(u => u.Login == userToAdd.Login));
		}

		[Fact]
		public void IsRemovedUserCantBeDisplayed()
		{
			// Arrange
			var mock = new Mock<IUserRelatedRepository>();
			mock.Setup(r => r.AllUsers).Returns(BuildTestUsers());

			var vm1 = new UserViewModel(mock.Object, null);

			var userLoginToRemove = "user1";

			// Act
			vm1.SelectedUser = vm1.UsersToShow.Single(u => u.Login == userLoginToRemove);
			vm1.RemoveUserCommand.Execute(null);

			// Assert
			Assert.Empty(vm1.UsersToShow.Where(u => u.Login == userLoginToRemove));
		}
	}
}
