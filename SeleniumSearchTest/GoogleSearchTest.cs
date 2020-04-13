using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject
{
    public class GoogleSearchTest
    {
        IWebDriver driver;
        private string URL = "https://www.google.com/";

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(URL);
        }

        [Test(Description = "Check Google search result items much search query")]
        public void Check_Selenium_Search_And_Click_First_Item()
        {
            //Test variables
            const string SearchQuery = "Selenium";
            const string FirstSearchItem = "(//h3[contains(text(), 'Selenium')])[1] /parent::*";
            string expectedUrl;

            //Actions for transefring to search result page
            var googleSearchField = driver.FindElement(By.CssSelector("[name='q']"));
            googleSearchField.SendKeys("Selenium");
            googleSearchField.SendKeys(Keys.Enter);

            //Check that driver is on required google page
            Assert.That(driver.Title.Contains(SearchQuery));

            //Get the url from first link
            expectedUrl = driver.FindElement(By.XPath(FirstSearchItem))
                .GetAttribute("href");

            //Click on first link
            driver.FindElement(By.XPath(FirstSearchItem)).Click();

            //Check that driver is on required page
            Assert.AreEqual(expectedUrl, driver.Url);           
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
