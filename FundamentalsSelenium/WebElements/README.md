# Selenium Nunit + C#

## Launching a browser using WebDriver
1. Using MSTest project
2. Installed dependencies 

Crate a new class called FirstTest.cs or renamed default UnitTest1.cs then insert the following code
```
using OpenQA.Selenium.Chrome;
namespace Example
[TestClass]
{  public class Tests
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
        }}}
```

## Web Elements
### Finding web elements
- findElement() returns a single WebElement, If no matching element is found, it throws a NoSuchElementException.
 ```     
 IWebElement element = driver.findElement(By.id("example"));
 ```

- findElements() returns a list of WebElements, If no matching elements are found, it returns an empty list (not null).
```           
IList<IWebElement> elements = driver.findElements(By.className("example"));
```

## Locators
[Documentation](https://www.selenium.dev/documentation/webdriver/elements/locators/)

|Locator  | Description | Code
| ------------- |:-------------:| -------------:|
| class name     | Locates elements whose class name contains the search value (compound class names are not permitted) | By.Name
| css selector     | Locates elements matching a CSS selector     | By.CssSelector
| id      | Locates elements whose ID attribute matches the search value     | By.Id
|name | Locates elements whose NAME attribute matches the search value | By.Name
| link text | Locates anchor elements whose visible text matches the search value | By.LinkText
| partial link text | Locates anchor elements whose visible text contains the search value. If multiple elements are matching, only the first one will be selected.| By.PartialLinkText
| xPath | Locates elements matching an XPath expression | By.XPath

## Interacting with web elements

[Documentation](https://www.selenium.dev/documentation/webdriver/elements/interactions/)

There are only 5 basic commands that can be executed on an element:
- click (applies to any element)
- send keys (only applies to text fields and content editable elements)
- clear (only applies to text fields and content editable elements)
- submit (only applies to form elements)
- select (see Select List Elements)
  - You must install “Selenium.WebDriver.Support”

## Information about web elements
[Documentation](https://www.selenium.dev/documentation/webdriver/elements/information/)

Is displayed

```c#
bool is_email_visible = driver.FindElement(By).Displayed;
```

Is Enabled

```c#
bool is_email_visible = driver.FindElement(By).Enabled;
```

Is Selected

```c#
bool is_email_visible = driver.FindElement(By).Enabled;
```

Tag Name

```c#
string attr = driver.FindElement(By).TagName;
```

Size and Position

```c#
var res = driver.FindElement(By);
// Return x and y coordinates referenced element
System.Console.WriteLine(res.Location);
// Returns height, width
System.Console.WriteLine(res.Size);
```

Get CSS Value

```c#
string cssValue = driver.FindElement(By).GetCssValue("value");
```

Text context

```c#
string text = driver.FindElement(By).Text;
```

Fetching Attributes or Properties

```c#
string valueInfo = driver.FindElement(By).GetAttribute("value");
```
 

