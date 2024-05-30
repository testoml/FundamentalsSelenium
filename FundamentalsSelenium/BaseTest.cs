using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundamentalsSelenium
{
    [TestClass]
    public class BaseTest
    {
        public IWebDriver driver;

        private void SetDriver() {
            driver = new ChromeDriver();
        }

        #region hooks
        [TestInitialize]
        public void Init() { 
            SetDriver();
        }

        [TestCleanup]
        public void Cleanup() { driver.Quit(); }
        #endregion
    }
}
