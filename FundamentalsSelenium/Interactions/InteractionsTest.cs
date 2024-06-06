using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;

namespace FundamentalsSelenium.Interactions
{
    [TestClass]
    public class InteractionsTest:BaseTest
    {
        private Interactions Interactions => new(driver);

        #region[Navigation]
        //Launching browser and open your website.
        [TestMethod]
        public void UrlTest()
        {

            driver.Url = "https://selenium.dev";  //Convenient 
            var title = driver.Title; //Title page
            //Validate Title page
            Assert.IsTrue(title == "Selenium");
        }

        //Launching browser and open your website.
        [TestMethod]
        public void TestNavigationCommands()
        {
            driver.Navigate().GoToUrl("https://selenium.dev"); //Longer
            var title = driver.Title; //Title page
            //Validate Title page
            Assert.IsTrue(title == "Selenium");

            //Back
            driver.Navigate().Back();
            title = driver.Title;
            Assert.AreEqual(title, "");

            //Forward
            driver.Navigate().Forward();
            title = driver.Title;
            Assert.AreEqual(title, "Selenium");

            //Refresh
            driver.Navigate().Refresh();
            title = driver.Title;
            Assert.AreEqual(title, "Selenium");

        }
        #endregion

        #region[Alerts]
        [TestMethod]
        public void AceptAlert()
        {
            driver.Url = "https://the-internet.herokuapp.com/javascript_alerts";
            Interactions.Alert(By.CssSelector("button[onclick='jsAlert()']"), Interactions.AlertAction.Accept); 
            var result = GetResult();
            //Assert
            Assert.AreEqual(result,"You successfully clicked an alert");
        }

        [TestMethod]
        public void DimissAlert()
        {
            driver.Url = "https://the-internet.herokuapp.com/javascript_alerts";
            Interactions.Alert(By.CssSelector("button[onclick='jsConfirm()']"), Interactions.AlertAction.Dismiss);
            var result = GetResult();
            //Assert
            Assert.AreEqual(result, "You clicked: Cancel");

        }

        [TestMethod]
        public void SendText()
        {
            driver.Url = "https://the-internet.herokuapp.com/javascript_alerts";
            Interactions.Alert(By.CssSelector("button[onclick='jsPrompt()']"), Interactions.AlertAction.SendText, "Demo test");
            //Assert
            var result = GetResult();
            Assert.AreEqual(result, "You entered: Demo test");

        }

        /// <summary>
        /// Get text result after action alert
        /// </summary>
        /// <returns>string</returns>
        private string GetResult()
        {
            var element = Wait.WaitForElementDisplayed(driver, By.Id("result"), TimeSpan.FromSeconds(5), 150);
            return element.Text;
        }

        #endregion

        #region[Cookies]
        [TestMethod]
        public void ManageCookie()
        {

            driver.Url = "https://example.com";

            //Add Cookie into current browser context
            driver.Manage().Cookies.AddCookie(new Cookie("foo", "data"));
            //Get name
            var cookieName = driver.Manage().Cookies.GetCookieNamed("foo");
            Assert.AreEqual(cookieName.Value, "data");
            //add 2 cookies and get all
            driver.Manage().Cookies.AddCookie(new Cookie("test1", "cookie1"));
            driver.Manage().Cookies.AddCookie(new Cookie("test2", "cookie2"));
            var cookies = driver.Manage().Cookies.AllCookies;
            Assert.IsTrue(cookies.Count == 3);
            //Delete cookies named
            driver.Manage().Cookies.DeleteCookieNamed("test1");
            cookies = driver.Manage().Cookies.AllCookies;
            Assert.IsTrue(cookies.Count == 2);
            //Delete all cookies
            driver.Manage().Cookies.DeleteAllCookies();
            cookies = driver.Manage().Cookies.AllCookies;
            Assert.IsTrue(cookies.Count == 0);

        }
        #endregion

        #region[Frames]
        [TestMethod]
        public void UsingIndex()
        {
            driver.Url = "https://the-internet.herokuapp.com/frames";
            IWebElement linkNearest = driver.FindElement(By.CssSelector("a[href='/nested_frames']"));
            linkNearest.Click();
            //Get list of frames
            //Thread.Sleep(1000);
            IList<IWebElement> iframes = Wait.WaitForListElementDisplayed(driver, By.TagName("frame"), TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(100));
            Assert.IsTrue(iframes.Count == 2);
            //Access frame by position
            driver.SwitchTo().Frame(iframes[0]);
            IList<IWebElement> iframes1 = Wait.WaitForListElementDisplayed(driver, By.TagName("frame"), TimeSpan.FromSeconds(10), TimeSpan.FromMilliseconds(100));
            Assert.IsTrue(iframes1.Count == 3);
            driver.SwitchTo().Frame(iframes1[2]);
            IWebElement body1 = driver.FindElement(By.TagName("body"));
            Assert.AreEqual(body1.Text, "RIGHT");
        }

        [TestMethod]
        public void UsingNameOrId()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/frames");
            IWebElement linkNearest = driver.FindElement(By.CssSelector("a[href='/iframe']"));
            linkNearest.Click();
            //Get list of frames
            Thread.Sleep(1000);
            //Access frame name
            driver.SwitchTo().Frame("mce_0_ifr");
            IWebElement body = driver.FindElement(By.CssSelector("body p"));
            Assert.AreEqual(body.Text, "Your content goes here.");
        }
        #endregion

        #region[Windows]
        [TestMethod]
        public void WindowHandleDemo()
        {
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");
            Wait.WaitPageLoad(driver, "The Internet", TimeSpan.FromSeconds(10));
            //Store the ID of the original window
            string currentWindow = driver.CurrentWindowHandle;
            Assert.IsTrue(driver.WindowHandles.Count == 1);
            //Open new window
            var element = Wait.WaitForElementDisplayed(driver, By.CssSelector("a[href='/windows/new']"), TimeSpan.FromSeconds(10), 150);
            element.Click();
            //Window handles
            IList<string> windowHandles = new List<string>(driver.WindowHandles);
            Assert.IsTrue(driver.WindowHandles.Count == 2);
            foreach (var handle in windowHandles)
            {
                if (currentWindow != handle)
                {
                    driver.SwitchTo().Window(currentWindow);
                    break;
                }
            }
            Assert.AreEqual(driver.Title , "The Internet");

        }
        #endregion
    }
}
