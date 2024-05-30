using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;

namespace FundamentalsSelenium.WebElements
{
    internal class WebElements
    {
        private IWebDriver driver;
        public WebElements(IWebDriver driver) => this.driver = driver;

        #region[Upload]
        /// <summary>
        /// File upload
        /// </summary>
        /// <param name="relativePath">path from directory file</param>
        /// <param name="byFile">input to locate file</param>
        /// <param name="bySubmit">submit button to upload file</param>
        public void Upload(string relativePath, By byFile, By bySubmit) {
            string baseDirectory = AppContext.BaseDirectory;
            //string relativePath = "../../../Resources/selenium-snapshot.png";
            string uploadFile = Path.GetFullPath(Path.Combine(baseDirectory, relativePath));
            //Locate the file input element By.Id("file-upload")
            IWebElement fileInput = driver.FindElement(byFile);
            //Send File
            fileInput.SendKeys(uploadFile);
            //Submit the form By.Id("file-submit"
            driver.FindElement(bySubmit).Click();
        }
        #endregion

        #region[Information Elements]
        /// <summary>
        /// Element is displayed
        /// </summary>
        /// <param name="by">locator</param>
        /// <returns>bool</returns>
        public bool ElementIsDisplayed(By by) {
            return driver.FindElement(by).Displayed;
        }

        /// <summary>
        /// Element is Enabled
        /// </summary>
        /// <param name="by">locator</param>
        /// <returns>bool</returns>
        public bool ElementIsEnabled(By by)
        {
            return driver.FindElement(by).Enabled;
        }

        /// <summary>
        /// Element is selected
        /// </summary>
        /// <param name="by">locator</param>
        /// <returns>bool</returns>
        public bool ElementIsSelected(By by)
        {
            return driver.FindElement(by).Selected;
        }

        /// <summary>
        /// Get tag name
        /// </summary>
        /// <param name="by">locator</param>
        /// <returns>string</returns>
        public string GetTagName(By by) {
            return driver.FindElement(by).TagName;
        }

        /// <summary>
        /// Get location and size
        /// </summary>
        /// <param name="by">locator</param>
        /// <returns></returns>
        public IList<Object> GetSizeLocation(By by) {
            var element = driver.FindElement(by);
            IList<Object> result = [element.Location, element.Size];
            return result;
        }

        /// <summary>
        /// Get css value Test
        /// </summary>
        /// <param name="by">locator</param>
        /// <returns></returns>
        public string GetCssValueTest(By by, string css) {
            return driver.FindElement(by).GetCssValue(css);
        }


        public string GetTextContext(By by)
        {
            return driver.FindElement(by).Text;
        }

        public string GetAttribute(By by, string attributeValue) {
            IWebElement emailTxt = driver.FindElement(by);
            //fetch the value property associated with the textbox
            string valueInfo = emailTxt.GetAttribute(attributeValue);
            return valueInfo;
        }
        #endregion

        #region [Interation with elements]
        public void Click(By by) { 
            driver.FindElement(by).Click();
        }

        public void ClearAndSendKey(By by, string value) { 
           IWebElement element = driver.FindElement(by);
           element.Clear();
           element.SendKeys(value);
        }

        /// <summary>
        /// Select option according to type and value
        /// </summary>
        /// <param name="by">By</param>
        /// <param name="type">Text, Value, Index, Deselect(text, value, index)</param>
        /// <param name="value">value that should be selected by specific type</param>
        public void SelectOption(By by, string type, string value)
        {
            IWebElement element = driver.FindElement(by);
            var select = new SelectElement(element);
            switch (type)
            {
                case "text":
                    select.SelectByText(value); break;
                case "value":
                    select.SelectByValue(value); break;
                case "index":
                    select.SelectByIndex(int.Parse(value)); break;
                default:
                    Assert.Fail("Options don't match correctly ");
                    break;
            }
        }

        public Dictionary<IList<IWebElement>, IWebElement[]> SelectMultipleOption(By by) {
            IWebElement element = driver.FindElement(by);
            var select = new SelectElement(element);
            IList<IWebElement> listOptions;
            listOptions = select.Options;
            IWebElement[] optionElements = [.. element.FindElements(By.TagName("option"))];
            Dictionary<IList<IWebElement>, IWebElement[]> dictionary;
            dictionary = new()
            {
                { listOptions, optionElements }
            };
            return dictionary;
            
        }

        public IList<IWebElement> SelectAllOptions(By by) {
            IWebElement element = driver.FindElement(by);
            var select = new SelectElement(element);
            IList<IWebElement> optionList = select.AllSelectedOptions;
            return optionList;
        }
        #endregion
    }
}
