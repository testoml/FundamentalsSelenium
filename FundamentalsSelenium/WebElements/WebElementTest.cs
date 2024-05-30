using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FundamentalsSelenium.WebElements
{
    [TestClass]
    public class WebElementTest : BaseTest
    {
        private WebElements Element => new(driver);

        #region FileUpload
        [TestMethod]
        public void FileUploadTest() {
            driver.Url = "https://the-internet.herokuapp.com/upload";
            Element.Upload("../../../Resources/img/selenium-snapshot.png", By.Id("file-upload"), By.Id("file-submit"));
            //Validate the file upload succefully
            //It's posible to use wait if you need it
            IWebElement fileName = Wait.FluentWait(driver, By.Id("uploaded-files"), TimeSpan.FromSeconds(2), TimeSpan.FromMicroseconds(150));
            Assert.AreEqual("selenium-snapshot.png", fileName.Text);
        }
        #endregion

        #region InformationElements
        [TestMethod]
        public void IsDisplayedTest()
        {
            //Navigate to the url
            driver.Url = "https://www.selenium.dev/selenium/web/inputs.html";
            //Get boolean value for is element display
            bool displayed = Element.ElementIsDisplayed(By.Name("email_input"));
            //Validate if element is displayed
            Assert.IsTrue(displayed);
        }

        //Element is enabled on a webpage. Return boolean
        [TestMethod]
        public void IsEnabledTest()
        {
            // Navigate to Url
            driver.Url = "https://www.selenium.dev/selenium/web/inputs.html";
            // Store the WebElement
            bool enabled = Element.ElementIsEnabled(By.Name("button_input"));
            //Validate if element is enabled
            Assert.IsTrue(enabled);
        }

        //Determinate if the element is selected or not
        [TestMethod]
        public void IsSelectedTest()
        {
            // Navigate to Url
            driver.Url = "https://www.selenium.dev/selenium/web/inputs.html";
            // Returns true if element ins checked else returns false
            bool selected = Element.ElementIsSelected(By.Name("checkbox_input"));
            //Validate if element is selected
            Assert.IsTrue(selected);
        }

        //It is used to fetch the TagName of the referenced Element which has the focus in the current browsing context.
        [TestMethod]
        public void TagNameTest()
        {
            // Navigate to Url
            driver.Url = "https://www.selenium.dev/selenium/web/inputs.html";
            // Returns TagName of the element
            string attr = Element.GetTagName(By.Name("email_input"));
            //Validate input is an input
            Assert.AreEqual(attr, "input");
        }

        //It is used to fetch the dimensions and coordinates of the referenced element.
        [TestMethod]
        public void SizeAndPositionTest()
        {
            // Navigate to Url
            driver.Url = "https://www.selenium.dev/selenium/web/inputs.html";
            var res = Element.GetSizeLocation(By.Name("range_input"));
            var location = res[0];
            var size = res[1];
            // Return x and y coordinates referenced element
            System.Console.WriteLine(location);
            // Returns height, width
            System.Console.WriteLine(size);
        }

        //Retrieves the value of specified computed style property of an element in the current browsing context.
        [TestMethod]
        public void GetCssValueTest()
        {
            // Navigate to Url
            driver.Url = "https://www.selenium.dev/selenium/web/colorPage.html";
            // Retrieves the computed style property 'color' of linktext
            string cssValue = Element.GetCssValueTest(By.Id("namedColor"), "background-color");
            // Validate value of cssValue
            Assert.AreEqual(cssValue, "rgba(0, 128, 0, 1)");
        }

        //Retrieves the rendered text of the specified element.
        [TestMethod]
        public void GetTextContext()
        {
            // Navigate to url
            driver.Url = "https://www.selenium.dev/selenium/web/linked_image.html";
            // Retrieves the text of the element
            string text = Element.GetTextContext(By.CssSelector("#link"));
            // Validate text
            Assert.AreEqual(text, "Click here for next page");
        }

        //Fetches the run time value associated with a DOM attribute. It returns the data associated with the DOM attribute or property of the element.
        [TestMethod]
        public void FetchingAttributesPropertiesTest()
        {
            //Navigate to the url
            driver.Url = "https://www.selenium.dev/selenium/web/inputs.html";
            //fetch the value property associated with the textbox
            string valueInfo = Element.GetAttribute(By.Name("email_input"),"value");
            // Validate value info
            Assert.AreEqual(valueInfo, "admin@localhost");
        }

        #endregion

        #region[Interation with elements]
        [TestMethod]
        public void SelectOptionTest() {
            driver.Url = "https://www.selenium.dev/selenium/web/formPage.html";
            var twoElement = driver.FindElement(By.CssSelector("option[value=two]"));
            var fourElement = driver.FindElement(By.CssSelector("option[value=four]"));
            var countElement = driver.FindElement(By.CssSelector("option[value='still learning how to count, apparently']"));
            //select by text
            Element.SelectOption(By.Name("selectomatic"), "text", "Four");
            Assert.IsTrue(fourElement.Selected);
            //Select by value
            Element.SelectOption(By.Name("selectomatic"), "value", "two");
            Assert.IsTrue(twoElement.Selected);
            //Select by index
            Element.SelectOption(By.Name("selectomatic"), "index", "3");
            Assert.IsTrue(countElement.Selected);

        }

        [TestMethod]
        public void SelectMultipleOption()
        {
            driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/formPage.html");
            var selectElement = driver.FindElement(By.Name("multi"));
            var select = new SelectElement(selectElement);

            var hamElement = driver.FindElement(By.CssSelector("option[value=ham]"));
            var gravyElement = driver.FindElement(By.CssSelector("option[value='onion gravy']"));
            var eggElement = driver.FindElement(By.CssSelector("option[value=eggs]"));
            var sausageElement = driver.FindElement(By.CssSelector("option[value='sausages']"));

            IList<IWebElement> optionList = select.Options;
            IWebElement[] optionElements = [.. selectElement.FindElements(By.TagName("option"))];
            CollectionAssert.AreEqual(optionElements, optionList.ToArray());

            IList<IWebElement> selectedOptionList = select.AllSelectedOptions;
            IWebElement[] expectedSelection = [eggElement, sausageElement];
            CollectionAssert.AreEqual(expectedSelection, selectedOptionList.ToArray());

            select.SelectByValue("ham");
            select.SelectByValue("onion gravy");
            Assert.IsTrue(hamElement.Selected);
            Assert.IsTrue(gravyElement.Selected);

            select.DeselectByValue("eggs");
            select.DeselectByValue("sausages");
            Assert.IsFalse(eggElement.Selected);
            Assert.IsFalse(sausageElement.Selected);

        }
        #endregion

    }
}
