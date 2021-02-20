using Moq;
using Xunit;
using InventoryManager.Models;
using InventoryManager.ViewModels;
using InventoryManager.Views;
using System;

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

            var vm1 = new AuthorizationViewModel(null, null);
            vm1.AuthorizingUser = userTryingToPass;
            vm1.InputtedPassword = "123321";

            var vm2 = new AuthorizationViewModel(null, null);
            vm2.AuthorizingUser = userTryingToPass;
            vm2.InputtedPassword = "123";

            // Act
            var result1 = vm1.IsUserPasswordCorrect();
            var result2 = vm2.IsUserPasswordCorrect();

            // Assert
            Assert.True(result1);
            Assert.False(result2);
        }
    }
}
