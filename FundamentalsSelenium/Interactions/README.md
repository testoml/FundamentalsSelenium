# Interactions

## Browser Navigation

[Documementation](https://www.selenium.dev/documentation/webdriver/interactions/navigation/)

[Example]()

Navigate to

```c#
//Convenient
driver.Url = "https://selenium.dev";
//Longer
driver.Navigate().GoToUrl("https://selenium.dev");
```

Back (Pressing the browser’s back)

```c#
driver.Navigate().Back();
```

Forward (Pressing the browser’s forward button)

```c#
 driver.Navigate().Forward();
```

Refresh (Refresh the current page)

```c#
driver.Navigate().Refresh();
```

## JavaScript alerts

### Alerts

[Documentation](https://www.selenium.dev/documentation/webdriver/interactions/alerts/)

[Example]()

Accept

```c#
//Store the alert in a variable
IAlert alert = driver.SwitchTo().Alert();
//Accept
alert.Accept();
```

Dismiss

```c#
//Store the alert in a variable
var alert = driver.SwitchTo().Alert();
alert.Dismiss();
```

Send Text

```c#
var alert = driver.SwitchTo().Alert();
alert.SendKeys("Demo test");
alert.Accept();
```

Get Text
```c#
var alert = driver.SwitchTo().Alert();
//Store the alert text in a variable
string text = alert.Text;
```

## Cookie
A cookie is a small piece of data that is sent from a website and stored in your computer. Cookies are mostly used to recognize the user and load the stored information.

[Documentation](https://www.selenium.dev/documentation/webdriver/interactions/cookies/)

[Example]()

Add cookie 

```c#
driver.Manage().Cookies.AddCookie(new OpenQA.Selenium.Cookie("foo", "data"));
```

Get Named Cookie

```
var cookieName = driver.Manage().Cookies.GetCookieNamed("foo");
```

Get All Cookies

```
var cookies = driver.Manage().Cookies.AllCookies;
```

Delete Cookie

```
//Delete cookies named
driver.Manage().Cookies.DeleteCookieNamed("test1");
//Delete all cookies
 driver.Manage().Cookies.DeleteAllCookies();
```

## Frames
[Documentation](https://www.selenium.dev/documentation/webdriver/interactions/frames/)

[Example]()

Using a Web Element

```
//Store the web element
IWebElement iframe = driver.FindElement(By.CssSelector("#modal>iframe"));
//Switch to the frame
driver.SwitchTo().Frame(iframe);
```

Using Name or ID

```
//Or using the name instead
driver.SwitchTo().Frame("myframe");
```

Leaving a Frame

```
// Return to the top level
driver.SwitchTo().DefaultContent();
```

## Windows and Tabs
[Documentation](https://www.selenium.dev/documentation/webdriver/interactions/windows/)

[Example]()

Get window handle

```
string currentWindow = driver.CurrentWindowHandle;
```

Fetch windows

```
IList<string> windowHandles = new List<string>(driver.WindowHandles);
```

Swich to window or tab

```
driver.SwitchTo().Window(NameOfWindow);
```