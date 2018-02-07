using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsStore.Components;
using SportsStore.Models;
using Xunit;

namespace SportsStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        
        [Fact]
        public void Can_Select_Categories()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                new Product {ProductID = 2, Name = "P2", Category = "Apples"},
                new Product {ProductID = 3, Name = "P3", Category = "Plums"},
                new Product {ProductID = 4, Name = "P4", Category = "Oranges"},
            }).AsQueryable());
            
            var target = new NavigationMenuViewComponent(mock.Object);
            
            // Act - Get set of categories
            var results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();
            
            // Assert
            /*Assert.True(Enumerable.SequenceEqual(
                new string[] {"Apples", "Oranges", "Plums"}, results
            ));*/ // One way of checking equality of a sequence - through Enumerable
            Assert.True(new [] {"Apples", "Oranges", "Plums"}.SequenceEqual(results)); // Extension method invocation
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            //Arrange
            var categoryToSelect = "Apples";
            
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Apples"},
                new Product {ProductID = 4, Name = "P2", Category = "Oranges"},
            }).AsQueryable());

            var target = new NavigationMenuViewComponent(mock.Object)
            {
                ViewComponentContext = new ViewComponentContext
                {
                    ViewContext = new ViewContext
                    {
                        RouteData = new RouteData()
                    }
                }
            };


            target.RouteData.Values["category"] = categoryToSelect;
            
            // Action
            var result = (string) (target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];
            
            // Assert
            Assert.Equal(categoryToSelect, result);
        }
    }
}