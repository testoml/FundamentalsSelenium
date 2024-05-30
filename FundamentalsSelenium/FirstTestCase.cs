using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundamentalsSelenium
{
    [TestClass]
    public  class FirstTestCase
    {
        [TestMethod]
        public void Test1()
        {
            //This line creates an instance of the Chrome WebDriver
            var driver = new ChromeDriver();
            //This line navigates the Chrome browser to the specified URL
            driver.Navigate().GoToUrl("https://www.google.com/");
            /*This line quits or closes the Chrome browser window and     terminates the WebDriver session.
             * It's important to clean up resources properly after the test automation is complete*/
            driver.Quit();
        }
    }
}
