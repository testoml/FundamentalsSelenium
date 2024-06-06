using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundamentalsSelenium
{
    public static class Wait
    {
        /// <summary>
        /// Implicit wait: This is a global setting that applies to every element location call for the entire session
        /// </summary>
        /// <param name="driver">Intance of driver</param>
        /// <param name="time">TimeSpan</param>
        public static void ImplicitWait(IWebDriver driver, TimeSpan time) {
            driver.Manage().Timeouts().ImplicitWait = time;
        }

        /// <summary>
        /// Explicit Wait: Wait for an spefic if an specific element is displayed
        /// </summary>
        /// <param name="driver">Instance of driver</param>
        /// <param name="by">name, id, tagname, cssSelector e.g(By.by(Id("")))</param>
        /// <param name="time">specify timespan</param>
        /// <returns></returns>
        public static IWebElement ExplicitWait(this IWebDriver driver, By by, TimeSpan time)
        {
            if (time.TotalMilliseconds > 0)
            {
                var wait = new WebDriverWait(driver, time);
                wait.Until(drv => drv.FindElement(by).Displayed);
            }

            return driver.FindElement(by);
        }

       /// <summary>
       /// 
      /// </summary>
      /// <param name="driver"></param>
      /// <param name="by"></param>
   /// <param name="timeWait"></param>
   /// <param name="timePollingInterval"></param>
   /// <returns></returns>
        public static IWebElement FluentWait(IWebDriver driver,  By by, TimeSpan timeWait, TimeSpan timePollingInterval)
        {

            WebDriverWait wait = new(driver, timeWait)
            {
                PollingInterval = timePollingInterval
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            IWebElement element = wait.Until((d) =>
            {
                return d.FindElement(by);
            });
            return element;
        }

        /// <summary>
        /// Validate if elements is displayed, enabled or selected - Returned bool
        /// </summary>
        /// <param name="element">element to validate</param>
        /// <param name="type">displayed, enabled, selected</param>
        /// <param name="time">Timespan</param>
        /// <returns>bool</returns>
        /// <exception cref="Exception"></exception>
        public static bool ValidateElementExists(IWebDriver driver, By by, string type, TimeSpan time)
        {

            var s = new Stopwatch();
            bool result = false;
            s.Start();
            while (s.Elapsed < time)
            {
                try
                {
                    switch (type) {
                        case "displayed":
                            if (driver.FindElement(by).Displayed) { return true; }
                        break;
                        case "enabled":
                            if (driver.FindElement(by).Enabled) { return true; }
                            break;
                        case "selected":
                            if (driver.FindElement(by).Selected) { return true; }
                            break;
                    }
                }
                catch (NoSuchElementException e)
                {
                    throw new Exception(e.Message);
                }
            }
            s.Stop();
            return result;
        }

        public static IWebElement WaitForElementDisplayed(IWebDriver driver, By by, TimeSpan timeWait, int timePollingInterval)
        {
            
            IWebElement element = FluentWait(driver, by, timeWait, TimeSpan.FromMilliseconds(timePollingInterval));
            bool exists;
            if (!element.Displayed)
            {
                exists = ValidateElementExists(driver, by, "displayed", timeWait);
                if (!exists) {
                    Assert.Fail("Element doesn't display according time specified");                
                          }
            }
            return element;
            
        }

        public static IList<IWebElement> WaitForListElementDisplayed(IWebDriver driver, By by, TimeSpan timeWait, TimeSpan timePollingInterval)
        {

            WebDriverWait wait = new(driver, timeWait)
            {
                PollingInterval = timePollingInterval
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            IList<IWebElement> elements = wait.Until((d) =>
            {
                foreach (var item in d.FindElements(by))
                {
                    if (!item.Displayed)
                    {
                        ValidateElementExists(driver, by, "displayed", timeWait);
                    }
                }
                return d.FindElements(by);
            });
           return elements;

        }

        /// <summary>
        /// Wait for page load after to display the title expected
        /// </summary>
        /// <param name="driver">Instance of driver</param>
        /// <param name="titleToCompare">Title expected</param>
        /// <param name="time">Timespan</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool WaitPageLoad(IWebDriver driver, string titleToCompare, TimeSpan time)
        {
            var s = new Stopwatch();
            bool result = false;
            s.Start();
            while (s.Elapsed < time)
            {
                try
                {
                    var title = driver.Title;
                    if (title == titleToCompare)
                    {
                        result = true;
                    }
                }
                catch (StaleElementReferenceException e)
                {
                    throw new Exception(e.Message);
                }
            }
            s.Stop();
            return result;
        }

        internal static object FluentWait(IWebDriver driver, By by)
        {
            throw new NotImplementedException();
        }
    }
}
