using Xunit;
using InventoryManager.Models;
using InventoryManager.ViewModels;

namespace InventoryManager.Tests
{
	public class AuthorizationTests
	{
		[Fact]
		public void CanUserBeAuthorized()
		{
			// Arrange
			var userTryingToPass = new User
			{
				Login = "Ivan123",
				Password = "123321"
			};

			var vm1 = new AuthorizationViewModel(null);
			vm1.AuthenticatedUser = userTryingToPass;
			vm1.InputtedPassword = "123321";

			var vm2 = new AuthorizationViewModel(null);
			vm2.AuthenticatedUser = userTryingToPass;
			vm2.InputtedPassword = "123";

			// Act
			var result1 = vm1.IsInputtedPasswordCorrect();
			var result2 = vm2.IsInputtedPasswordCorrect();

			// Assert
			Assert.True(result1);
			Assert.False(result2);
		}
	}
}
