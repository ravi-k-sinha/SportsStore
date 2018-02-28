using ControllersAndActions.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

namespace ControllersAndActions.Tests
{
    public class ActionTests
    {
        [Fact]
        public void ViewSelected()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.ReceiveForm("Adam", "London");

            // Assert
            Assert.Equal("Result", result.ViewName);
        }

        [Fact]
        public void DefaultViewSelected()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.DefaultView();

            // Assert
            Assert.Null(result.ViewName);
        }

        [Fact]
        public void ModelObjectType()
        {
            // Arrange
            ExampleController controller = new ExampleController();

            // Act
            ViewResult result = controller.Index();

            // Assert
            Assert.IsType<string>(result.ViewData["Message"]);
            Assert.Equal("Hello", result.ViewData["Message"]);
            Assert.IsType<DateTime>(result.ViewData["Date"]);
        }

        [Fact]
        public void Redirection()
        {
            // Arrange
            ExampleController controller = new ExampleController();

            // Act
            RedirectResult result = controller.Redirect();

            // Assert
            Assert.Equal("/Example/Index", result.Url);            
            Assert.True(result.Permanent);
        }
    }
}
