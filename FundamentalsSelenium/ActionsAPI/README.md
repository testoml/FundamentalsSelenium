# Actions API

## Keyboard Actions

**Keys**
It could not implement.


**Mouse**
- [Documentation](https://www.selenium.dev/documentation/webdriver/actions_api/mouse/)
- [GitHub]()

*Click and Release*

```c#
new Actions(driver).Click(element).Perform();
```

*Double-clicking an element*

```c#
new Actions(driver).DoubleClick (element).Perform();
```

*Right-clicking an element*

```c#
new Actions(driver).ContextClick(element).Perform();
```

*Hovering over an element*

```c#
new Actions(driver).MoveElement(element).Perform();
```

*Dragging and dropping an  element*

```c#
new Actions(driver).DragAndDrop(elementDraggable, elementDroppable).Perform();
```

*Performing a series of actions sequentially*

```c#
new Actions(driver);
.MoveToElement(element).Click().SendKeys("Text").DoubleClick()
.ContextClick()
.Build() .Perform();
```

**ActionBuilder**
Provides methods that allow the creation of actions sequences to enable advanced user interaction

*Click Back*
```c#
ActionBuilder actionBuilder = new();
 PointerInputDevice mouse = new (PointerKind.Mouse, "default mouse");
actionBuilder.AddAction(mouse.CreatePointerDown(MouseButton.Back));
actionBuilder.AddAction(mouse.CreatePointerUp(MouseButton.Back));
((IActionExecutor)driver).PerformActions(actionBuilder.ToActionSequenceList());

```

**Pen actions**
A representation of a pen stylus kind of pointer input for interacting with a web page.
- [Documentation](https://www.selenium.dev/documentation/webdriver/actions_api/pen/)
- [GitHub]()

*Using Pen*+
```c#
 IWebElement pointerArea = driver.FindElement(By.Id("pointerArea"));
 ActionBuilder actionBuilder = new();//allow create an action
 PointerInputDevice pen = new(PointerKind.Pen, "default pen"); //represent mouse, finger

 actionBuilder.AddAction(pen.CreatePointerMove(pointerArea, 0, 0, TimeSpan.FromMilliseconds(800)));
 actionBuilder.AddAction(pen.CreatePointerDown(MouseButton.Left));
 actionBuilder.AddAction(pen.CreatePointerMove(CoordinateOrigin.Pointer,2, 2, TimeSpan.Zero));
 actionBuilder.AddAction(pen.CreatePointerUp(MouseButton.Left));
 ((IActionExecutor)driver).PerformActions(actionBuilder.ToActionSequenceList());

 var moves = driver.FindElements(By.ClassName("pointermove"));
 var moveTo = GetProperties(moves.ElementAt(0));
 var down = GetProperties(driver.FindElement(By.ClassName("pointerdown")));
 var moveBy = GetProperties(moves.ElementAt(1));
 var up = GetProperties(driver.FindElement(By.ClassName("pointerup")));

 Point location = pointerArea.Location;
 Size size = pointerArea.Size;
 decimal centerX = location.X + size.Width / 2;
 decimal centerY = location.Y + size.Height / 2;
```

*Adding Pointer Event Attributes*
```c#
        IWebElement pointerArea = driver.FindElement(By.Id("pointerArea"));
            ActionBuilder actionBuilder = new();
            PointerInputDevice pen = new(PointerKind.Pen, "default pen");
            PointerInputDevice.PointerEventProperties properties = new()
            {
                TiltX = -72,
                TiltY = 9,
                Twist = 86,
            };
            actionBuilder.AddAction(pen.CreatePointerMove(pointerArea, 0, 0, TimeSpan.FromMilliseconds(800)));
            actionBuilder.AddAction(pen.CreatePointerDown(MouseButton.Left));
            actionBuilder.AddAction(pen.CreatePointerMove(CoordinateOrigin.Pointer,
                2, 2, TimeSpan.Zero, properties));
            actionBuilder.AddAction(pen.CreatePointerUp(MouseButton.Left));
            ((IActionExecutor)driver).PerformActions(actionBuilder.ToActionSequenceList());
```

**Scroll wheel actions**
- [Documentation](https://www.selenium.dev/documentation/webdriver/actions_api/wheel/)
- [GitHub]()
-
*Scroll to element*

```c#
 IWebElement iframe = driver.FindElement(By.TagName("iframe"));
 new Actions(driver)
 .ScrollToElement(iframe)
 .Perform();
```

*Scroll by given amount*

Pass in an delta x and a delta y value for how much to scroll in the right and down directions. Negative values represent left and up, respectively.

```c#
 IWebElement footer = driver.FindElement(By.TagName("footer"));
 int deltaY = footer.Location.Y;
 new Actions(driver)
 .ScrollByAmount(0, deltaY)
 .Perform();
```

*Scroll from an element by a given amount*

```c#
 IWebElement iframe = driver.FindElement(By.TagName("iframe"));
 WheelInputDevice.ScrollOrigin scrollOrigin = new WheelInputDevice.ScrollOrigin
 { Element = iframe};
 new Actions(driver)
 .ScrollFromOrigin(scrollOrigin, 0, 200)
 .Perform();
```

*Scroll from an element with an offset*

```c#
 WebElement footer = driver.FindElement(By.TagName("footer"));
            var scrollOrigin = new WheelInputDevice.ScrollOrigin
            {
                Element = footer,
                XOffset = 0,
                YOffset = -50
            };
            new Actions(driver)
                .ScrollFromOrigin(scrollOrigin, 0, 200)
                .Perform();
```