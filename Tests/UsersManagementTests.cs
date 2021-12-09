using Xunit;
using Moq;
using InventoryManager.Models;
using InventoryManager.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System;

namespace InventoryManager.Tests
{
	public class UsersManagementTests
	{
		private IEnumerable<User> BuildTestUsers()
		{
			var userGroups = new UserGroup[] {
				new UserGroup() {
					ID = Guid.NewGuid(),
					Name = "Tech"
				},
				new UserGroup() {
					ID = Guid.NewGuid(),
					Name = "Adm"
				}
			};

			var users = new User[]
			{
				new User
				{
					Login = "user1",
					Password = "user1password",
					FirstName = "TestFirstName1",
					LastName = "TestLastName1",
					MiddleName = "TestMiddleName1",
					UserGroupID = userGroups[0].ID
				},
				new User
				{
					Login = "user2",
					Password = "user2password",
					FirstName = "TestFirstName2",
					LastName = "TestLastName2",
					MiddleName = "TestMiddleName2",
					UserGroupID = userGroups[1].ID
				}
			};

			return users;
		}

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
			addUserVM.SelectedUserGroup = new UserGroup { ID = Guid.Empty };

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
