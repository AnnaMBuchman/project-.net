using System;
using Xunit;
using projekt1.net;
using projekt1.net.Models;
using projekt1.net.Controllers;
using projekt1.net.EntityFramework;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace XUnitTestProject1
{
    public class UnitTest1 
    {
        
            [Fact(DisplayName = "Index should return default view")]
        public async Task IndexShouldReturnDefaultView()
        {
            
            var controller = new HomeController();

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        protected DataContext GetContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            return new DataContext(options);
        }
    }
    
}
