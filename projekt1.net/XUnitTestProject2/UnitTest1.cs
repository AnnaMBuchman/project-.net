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
using System.Collections.Generic;
using Moq;
namespace XUnitTestProject2
{
    public class UnitTest1
    {
        [Fact(DisplayName = "Index should return default view")]
        public void IndexShouldReturnDefaultView()
        {

            var controller = new HomeController();

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
            Assert.True(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Index");

        }
        [Fact(DisplayName = "Privacy should return default view")]
        public void JobOfferIndexShouldReturnDefaultView()
        {

            var controller = new HomeController();

            // Act
            var result = controller.Privacy();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
            Assert.True(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Index");

        }
        //public interface IBrainstormSessionRepository
        //{
        //    Task<BrainstormSession> GetByIdAsync(int id);
        //    Task<List<BrainstormSession>> ListAsync();
        //    Task AddAsync(BrainstormSession session);
        //    Task UpdateAsync(BrainstormSession session);
        //}
        [Fact(DisplayName = "Index without user should raise null reference exception")]
        public async System.Threading.Tasks.Task IndexWithoutUserShouldRaiseNullReferenceException()
        {
            DataContext context = GetContext();
            JobOfferController controller = new JobOfferController(context, null, null,null);
            JobOffer jobOffer=new JobOffer();
            await Assert.ThrowsAsync<NullReferenceException>(() => controller.Edit(jobOffer));
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
