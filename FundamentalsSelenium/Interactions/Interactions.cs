using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
namespace FundamentalsSelenium.Interactions
{
    internal class Interactions
    {
        public IWebDriver driver;

        public Interactions(IWebDriver driver) => this.driver = driver;

        public IAlert Alert(By by, AlertAction action, string sendText = "") { 
              
              driver.FindElement(by).Click();
              IAlert alert = driver.SwitchTo().Alert();
              switch (action) {
                case AlertAction.Accept:
                    alert.Accept();
                    break;
                case AlertAction.Dismiss:
                    alert.Dismiss();
                    break;
                case AlertAction.SendText:
                    alert.SendKeys(sendText);
                    alert.Accept();
                    break;
                default:
                    Assert.Fail("Any coincidence related to action alert expect: accept, dimiss, sentText");
                break;
              }
            return alert;
        }

       
        public enum AlertAction { 
         Accept,
         Dismiss,
         SendText
        }

      


    }
}
