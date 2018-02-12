using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            // Arrange - Create a mock repository
            var mock = new Mock<IOrderRepository>();
            
            // Arrange - Create an empty cart
            var cart = new Cart();
            
            // Arrange - Create the order
            var order = new Order();
            
            // Arrange - Create an instance of the controller
            var target = new OrderController(mock.Object, cart);
            
            // Act
            var result = target.Checkout(order) as ViewResult;
            
            // Assert - Check that the order hasn't been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            
            // Assert - Check that method is returning the default view
            Assert.True(string.IsNullOrEmpty(result.ViewName));
            
            // Assert - Check that I am passing an invalid model to the view
            Assert.False(result.ViewData.ModelState.IsValid);
        }
        
        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // Arrange - Create a mock repository
            var mock = new Mock<IOrderRepository>();
            
            // Arrange - Create an empty cart
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            
            // Arrange - Create the order
            var order = new Order();
            
            // Arrange - Create an instance of the controller
            var target = new OrderController(mock.Object, cart);
            target.ModelState.AddModelError("error", "error"); // Arrange - add an error to the model
            
            // Act - Try to checkout
            var result = target.Checkout(order) as ViewResult;
            
            // Assert - Check that the order hasn't been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            
            // Assert - Check that method is returning the default view
            Assert.True(string.IsNullOrEmpty(result?.ViewName));
            
            // Assert - Check that I am passing an invalid model to the view
            Assert.False(result != null && result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange - Create a mock order repository
            var mockRepo = new Mock<IOrderRepository>();
            
            // Arrange - Create a cart with one item
            var cart = new Cart();
            cart.AddItem(new Product(), 1);
            
            // Arrange - Create an instance of the controller
            var target = new OrderController(mockRepo.Object, cart);
            
            // Act - Try to checkout
            var result = target.Checkout(new Order()) as RedirectToActionResult;
            
            // Assert - Check that the order has been stored
            mockRepo.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);

            // Assert - Check that the method is redirecting to the Completed action
            Assert.Equal("Completed", result?.ActionName);

        }
    }
}