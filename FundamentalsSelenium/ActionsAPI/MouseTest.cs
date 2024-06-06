using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundamentalsSelenium;

namespace FundamentalsSeleniumNUnit.ActionsAPI
{
    [TestClass]
    public class MouseTest:BaseTest
    {
        private readonly string message = "Message expected is incorrect";
       
        [TestMethod]
        public void ClickAndHold()
        {
            driver.Url = "https://selenium.dev/selenium/web/mouse_interaction.html";
            IWebElement clickable = driver.FindElement(By.Id("clickable"));
            new OpenQA.Selenium.Interactions.Actions(driver)
             .ClickAndHold(clickable)
             .Perform();
            IWebElement status = driver.FindElement(By.Id("click-status"));
            Assert.AreEqual(status.Text, "focused", message);
        }

        [TestMethod]
        public void ClickAndRelease()
        {
            driver.Url = "https://selenium.dev/selenium/web/mouse_interaction.html";
            IWebElement clickable = driver.FindElement(By.Id("click"));
            new OpenQA.Selenium.Interactions.Actions(driver)
             .Click(clickable)
             .Perform();
            string getUrl = driver.Url;
            Assert.IsTrue(getUrl.Contains("resultPage.html"));
        }

        [TestMethod]
        public void DobleClick()
        {
            driver.Url = "https://selenium.dev/selenium/web/mouse_interaction.html";
            IWebElement clickable = driver.FindElement(By.Id("clickable"));
            new OpenQA.Selenium.Interactions.Actions(driver)
             .DoubleClick(clickable)
             .Perform();
            IWebElement status = driver.FindElement(By.Id("click-status"));
            Assert.AreEqual(status.Text, "double-clicked", message);

        }

        [TestMethod]
        public void RightClick()
        {
            driver.Url = "https://selenium.dev/selenium/web/mouse_interaction.html";
            IWebElement clickable = driver.FindElement(By.Id("clickable"));
            new OpenQA.Selenium.Interactions.Actions(driver)
             .ContextClick(clickable)
             .Perform();
            IWebElement status = driver.FindElement(By.Id("click-status"));
            Assert.AreEqual(status.Text, "context-clicked", message);

        }

        [TestMethod]
        public void Hovers()
        {
            driver.Url = "https://selenium.dev/selenium/web/mouse_interaction.html";
            IWebElement hoverable = driver.FindElement(By.Id("hover"));
            new Actions(driver)
                .MoveToElement(hoverable)
                .Perform();
            IWebElement status = driver.FindElement(By.Id("move-status"));
            Assert.AreEqual(status.Text, "hovered", message);

        }


        [TestMethod]
        public void DragToElement()
        {
            driver.Url = "https://selenium.dev/selenium/web/mouse_interaction.html";
            IWebElement draggable = driver.FindElement(By.Id("draggable"));
            IWebElement droppable = driver.FindElement(By.Id("droppable"));
            new OpenQA.Selenium.Interactions.Actions(driver)
              .DragAndDrop(draggable, droppable)
              .Perform();
            IWebElement status = driver.FindElement(By.Id("drop-status"));
            Assert.AreEqual(status.Text, "dropped", message);
        }

        [TestMethod]
        public void BackClick()
        {
            driver.Url = "https://selenium.dev/selenium/web/mouse_interaction.html";
            IWebElement click = driver.FindElement(By.Id("Click"));
            click.Click();
            Assert.AreEqual(driver.Title, "We Arrive Here", message);

            ActionBuilder actionBuilder = new();
            PointerInputDevice mouse = new(PointerKind.Mouse, "default mouse");
            actionBuilder.AddAction(mouse.CreatePointerDown(MouseButton.Back));
            actionBuilder.AddAction(mouse.CreatePointerUp(MouseButton.Back));
            ((IActionExecutor)driver).PerformActions(actionBuilder.ToActionSequenceList());

            Assert.AreEqual(driver.Title, "BasicMouseInterfaceTest", message);
        }

        [TestMethod]
        public void MoveByOffsetFromCenterOfElement()
        {
            driver.Url = "https://selenium.dev/selenium/web/mouse_interaction.html";
            IWebElement tracker = driver.FindElement(By.Id("mouse-tracker"));
            new OpenQA.Selenium.Interactions.Actions(driver)
            .MoveToElement(tracker, 8, 0)
            .Perform();
            string[] result = driver.FindElement(By.Id("relative-location")).Text.Split(", ");
            var re = Math.Abs(int.Parse(result[0]) - 100 - 8);
            Assert.IsTrue( re < 2);
        }
    }
}
