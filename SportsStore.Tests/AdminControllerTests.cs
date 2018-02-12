using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void Index_Contains_All_Products()
        {
            // Arrange - Create the mock repository
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            }.AsQueryable());
            
            // Arrange - Create a controller
            var target = new AdminController(mockRepo.Object);
            
            // Action
            var result = GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();
            
            // Assert
            Assert.Equal(3, result.Length);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);

        }

        [Fact]
        public void Can_Edit_Product()
        {
            // Arrange - Create the mock repository
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            }.AsQueryable());
            
            // Arrange - Create a controller
            var target = new AdminController(mockRepo.Object);
            
            // Act
            var p1 = GetViewModel<Product>(target.Edit(1));
            var p2 = GetViewModel<Product>(target.Edit(2));
            var p3 = GetViewModel<Product>(target.Edit(3));
            
            // Assert
            Assert.Equal(1, p1.ProductID);
            Assert.Equal(2, p2.ProductID);
            Assert.Equal(3, p3.ProductID);
        }

        [Fact]
        public void Cannot_Edit_NonExistent_Product()
        {
            // Arrange - Create the mock repository
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            }.AsQueryable());
            
            // Arrange - Create a controller
            var target = new AdminController(mockRepo.Object);
            
            // Act
            var p4 = GetViewModel<Product>(target.Edit(4));
            
            // Assert
            Assert.Null(p4);
        }

        [Fact]
        public void Can_Save_Valid_Changes()
        {
            // Arrange - Create Mock repository
            var mockRepo = new Mock<IProductRepository>();
            
            // Arrange - Create mock temp data
            var mockTemp = new Mock<ITempDataDictionary>();
            
            // Arrange - Create the controller
            var target = new AdminController(mockRepo.Object)
            {
                TempData = mockTemp.Object
            };
            
            // Arrange - Create a product
            var product = new Product {Name = "Test"};
            
            // Act - Try to save the product
            var result = target.Edit(product);
            
            // Assert - Check that the repository was called
            mockRepo.Verify(m => m.SaveProduct(product));
            
            // Assert - Check that the result type is a redirection
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult)?.ActionName);
        }

        [Fact]
        public void Cannot_Save_Invalid_Changes()
        {
            // Arrange - Create Mock repository
            var mockRepo = new Mock<IProductRepository>();
            
            // Arrange - Create the controller
            var target = new AdminController(mockRepo.Object);
            
            // Arrange - Create a product
            var product = new Product {Name = "Test"};
            
            // Arrange - Add an error to the model state
            target.ModelState.AddModelError("error", "error");
            
            // Act - Try to save the product
            var result = target.Edit(product);
            
            // Assert - Check that the repository was called
            mockRepo.Verify(m => m.SaveProduct(product), Times.Never);
            
            // Assert - Check that the result type is View Result
            Assert.IsType<ViewResult>(result);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}