using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace JobOfferApp.AutomatedUITests
{
    public class AutomatedUITests:IDisposable
    {
        private readonly IWebDriver _driver;
        public AutomatedUITests()
        {
            _driver = new ChromeDriver(@"C:\Users\48502\source\repos\first2\projekt1.net\JobOfferApp.AutomatedUITests\bin\Debug\netcoreapp3.1");
        }
        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
        
        [Fact]
        public void Navbar_WhenExecuted_Return()
        {
           _driver.Navigate().GoToUrl("https://localhost:5001/");
            var element = _driver.FindElement(By.Id("h"));
            Assert.Equal("Home", element.Text);

        }
        [Fact]
        public void Home_WhenExecuted_ContainsProjekt()
        {
            _driver.Navigate().GoToUrl("https://localhost:5001");
            var element = _driver.FindElement(By.ClassName("navbar-brand"));
            Assert.Equal("Projekt1", element.Text);
        }
        [Fact]
        public void Home_WhenExecuted_Contains()
        {
            _driver.Navigate().GoToUrl("https://localhost:5001");
            
            Assert.Contains("Id", _driver.PageSource);
           
        }
    }
}
