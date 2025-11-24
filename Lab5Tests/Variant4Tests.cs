using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using OpenQA.Selenium.Interactions;

namespace Lab5Tests
{
    /// <summary>
    /// Клас для виконання тестових сценаріїв Варіанту 4.
    /// </summary>
    public class Variant4Tests : SeleniumTests
    {
        /// <summary>
        /// Тест 4.1: Перевірка функціоналу сторінки Add/Remove Elements.
        /// Сценарій: Додавання елемента кнопкою "Add Element" та його видалення кнопкою "Delete".
        /// </summary>
        [Test]
        public void AddRemoveElementsTest()
        {
            // Перехід на сторінку
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/add_remove_elements/");

            // Знаходимо кнопку "Add Element" та натискаємо її
            driver.FindElement(By.XPath("//button[text()='Add Element']")).Click();

            // Перевіряємо, що з'явилася кнопка "Delete" (клас 'added-manually')
            IWebElement deleteButton = driver.FindElement(By.ClassName("added-manually"));
            Assert.That(deleteButton.Displayed, Is.True, "Кнопка 'Delete' має відображатися після додавання.");

            // Видаляємо елемент
            deleteButton.Click();

            // Перевіряємо, що кнопка "Delete" зникла з DOM
            ReadOnlyCollection<IWebElement> deletedButtons = driver.FindElements(By.ClassName("added-manually"));
            Assert.That(deletedButtons.Count, Is.EqualTo(0), "Кнопка 'Delete' має зникнути після натискання.");
        }

        /// <summary>
        /// Тест 4.2: Перевірка роботи чекбоксів.
        /// Сценарій: Перевірка початкового стану, зміна стану кліком та фінальна перевірка.
        /// </summary>
        [Test]
        public void CheckboxesTest()
        {
            // Перехід на сторінку
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/checkboxes");

            // Отримуємо список усіх чекбоксів на формі (їх має бути 2)
            var checkboxes = driver.FindElements(By.CssSelector("#checkboxes input[type='checkbox']"));

            // --- Робота з першим чекбоксом (індекс 0) ---
            var checkbox1 = checkboxes[0];
            // Перевіряємо, що він за замовчуванням вимкнений
            Assert.That(checkbox1.Selected, Is.False, "Чекбокс 1 має бути вимкнений за замовчуванням.");
            
            // Клікаємо (вмикаємо)
            checkbox1.Click();
            
            // Перевіряємо, що він увімкнувся
            Assert.That(checkbox1.Selected, Is.True, "Чекбокс 1 не увімкнувся після кліку.");

            // --- Робота з другим чекбоксом (індекс 1) ---
            var checkbox2 = checkboxes[1];
            // Перевіряємо, що він за замовчуванням увімкнений
            Assert.That(checkbox2.Selected, Is.True, "Чекбокс 2 має бути увімкнений за замовчуванням.");
            
            // Клікаємо (вимикаємо)
            checkbox2.Click();
            
            // Перевіряємо, що він вимкнувся
            Assert.That(checkbox2.Selected, Is.False, "Чекбокс 2 не вимкнувся після кліку.");
        }

        /// <summary>
        /// Тест 4.3: Перевірка роботи випадаючого списку (Dropdown).
        /// Сценарій: Вибір опцій за текстом та перевірка поточного вибраного значення.
        /// </summary>
        [Test]
        public void DropdownTest()
        {
            // Перехід на сторінку
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/dropdown");

            // Знаходимо елемент <select> за його ID
            var dropdownElement = driver.FindElement(By.Id("dropdown"));

            // Створюємо об'єкт SelectElement для зручної роботи з опціями
            var selectObject = new SelectElement(dropdownElement);

            // --- Вибір першої опції ---
            // Вибираємо опцію за відображуваним текстом "Option 1"
            selectObject.SelectByText("Option 1");
            
            // Перевіряємо, що вибрана саме ця опція
            Assert.That(selectObject.SelectedOption.Text, Is.EqualTo("Option 1"), "Опція 1 не вибралась.");

            // --- Вибір другої опції ---
            // Вибираємо опцію за текстом "Option 2"
            selectObject.SelectByText("Option 2");

            // Перевіряємо, що вибір змінився на другу опцію
            Assert.That(selectObject.SelectedOption.Text, Is.EqualTo("Option 2"), "Опція 2 не вибралась.");
        }

        /// <summary>
        /// Тест 4.4: Перевірка поля введення (Inputs).
        /// Сценарій: Введення числового значення в поле input та перевірка його відображення.
        /// </summary>
        [Test]
        public void InputsTest()
        {
            // Перехід на сторінку
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/inputs");

            // Знаходимо поле input (воно там одне, тег input)
            var inputField = driver.FindElement(By.TagName("input"));

            // Вводимо число "12345"
            // Важливо: для полів вводу краще спочатку викликати Clear(), щоб очистити попередні значення
            inputField.Clear();
            inputField.SendKeys("12345");

            // Отримуємо поточне значення поля через атрибут "value"
            string inputValue = inputField.GetAttribute("value") ?? string.Empty;

            // Перевіряємо, що значення збереглося коректно
            Assert.That(inputValue, Is.EqualTo("12345"), "Значення в полі input не відповідає введеному.");
        }

        /// <summary>
        /// Тест 4.5: Перевірка кодів стану HTTP (Status Codes).
        /// Сценарій: Перехід на сторінку, вибір коду 200 та перевірка повідомлення про успішний статус.
        /// </summary>
        [Test]
        public void StatusCodesTest()
        {
            // Перехід на сторінку
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/status_codes");

            // Знаходимо посилання з текстом "200" і клікаємо по ньому
            driver.FindElement(By.LinkText("200")).Click();

            // Знаходимо параграф, де відображається результат (тег <p>)
            // Текст виглядає приблизно так: "This page returned a 200 status code.\n\n..."
            string resultText = driver.FindElement(By.CssSelector("div.example p")).Text;

            // Перевіряємо, що в тексті міститься фраза про 200 статус
            Assert.That(resultText, Does.Contain("200 status code"), "Повідомлення не містить очікуваний код статусу 200.");
        }

        /// <summary>
        /// Тест 4.6: Перевірка функціоналу Drag and Drop.
        /// Сценарій: Перетягування елемента A на місце елемента B за допомогою JS-скрипта (через особливості HTML5).
        /// </summary>
        [Test]
        public void DragAndDropTest()
        {
            // Перехід на сторінку
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/drag_and_drop");

            // Знаходимо колонки A та B
            var columnA = driver.FindElement(By.Id("column-a"));
            var columnB = driver.FindElement(By.Id("column-b"));

            // Перевіряємо початковий стан (A зліва, B справа)
            Assert.That(columnA.Text, Is.EqualTo("A"), "Початковий текст колонки A некоректний.");
            Assert.That(columnB.Text, Is.EqualTo("B"), "Початковий текст колонки B некоректний.");

            // ВИКОРИСТАННЯ JS EXECUTOR (Вирішення технічного ризику HTML5 D&D)
            // Стандартний Actions.DragAndDrop тут не працює, тому емулюємо події браузера
            var js = (IJavaScriptExecutor)driver;
            string javaScript = @"
                function createEvent(typeOfEvent) {
                    var event = document.createEvent('CustomEvent');
                    event.initCustomEvent(typeOfEvent, true, true, null);
                    event.dataTransfer = {
                        data: {},
                        setData: function (key, value) { this.data[key] = value; },
                        getData: function (key) { return this.data[key]; }
                    };
                    return event;
                }
                function dispatchEvent(element, event, transferData) {
                    if (transferData !== undefined) {
                        event.dataTransfer = transferData;
                    }
                    if (element.dispatchEvent) {
                        element.dispatchEvent(event);
                    } else if (element.fireEvent) {
                        element.fireEvent('on' + event.type, event);
                    }
                }
                function simulateHTML5DragAndDrop(element, destination) {
                    var dragStartEvent = createEvent('dragstart');
                    dispatchEvent(element, dragStartEvent);
                    var dropEvent = createEvent('drop');
                    dispatchEvent(destination, dropEvent, dragStartEvent.dataTransfer);
                    var dragEndEvent = createEvent('dragend');
                    dispatchEvent(element, dragEndEvent, dropEvent.dataTransfer);
                }
                simulateHTML5DragAndDrop(arguments[0], arguments[1]);
            ";

            // Виконуємо скрипт: перетягуємо A (arguments[0]) на B (arguments[1])
            js.ExecuteScript(javaScript, columnA, columnB);

            // Перевіряємо результат: текст у першій колонці (яка має ID column-a) має змінитися на "B"
            // Це підтверджує, що елементи помінялися місцями (контентом)
            Assert.That(columnA.Text, Is.EqualTo("B"), "Drag and Drop не спрацював: текст колонки A не змінився.");
            Assert.That(columnB.Text, Is.EqualTo("A"), "Drag and Drop не спрацював: текст колонки B не змінився.");
        }

        /// <summary>
        /// Тест 4.7: Перевірка змінного контенту (Shifting Content).
        /// Сценарій: Перехід до прикладу меню та перевірка кількості елементів списку.
        /// </summary>
        [Test]
        public void ShiftingContentTest()
        {
            // Перехід на головну сторінку розділу
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/shifting_content");

            // Переходимо до прикладу 1: "Menu Element"
            driver.FindElement(By.PartialLinkText("Example 1: Menu Element")).Click();

            // Знаходимо всі пункти меню (теги <li> всередині <ul>)
            var menuItems = driver.FindElements(By.CssSelector("ul li"));

            // Перевіряємо, що меню містить рівно 5 елементів (Contact, About, Home, Portfolio, Gallery)
            Assert.That(menuItems.Count, Is.EqualTo(5), "Кількість пунктів меню не відповідає очікуваній.");
        }

        /// <summary>
        /// Тест 4.8: Перевірка геолокації (Geolocation).
        /// Сценарій: Натискання кнопки визначення місця та перевірка відображених координат.
        /// Очікується, що координати співпадуть із тими, що ми задали в Setup (Лондон).
        /// </summary>
        [Test]
        public void GeolocationTest()
        {
            // Перехід на сторінку
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/geolocation");

            // Знаходимо кнопку "Where am I?" і клікаємо
            driver.FindElement(By.XPath("//button[contains(text(),'Where am I')]")).Click();

            // Чекаємо (неявно) появи координат і зчитуємо їх
            var latText = driver.FindElement(By.Id("lat-value")).Text;
            var longText = driver.FindElement(By.Id("long-value")).Text;

            // Перевіряємо, що координати відповідають нашим "фейковим" налаштуванням (Setup)
            // Це підтверджує, що механізм емуляції працює і реальна IP-адреса не використовується
            Assert.That(latText, Is.EqualTo("51.5055"), "Широта не відповідає очікуваній (Mock).");
            Assert.That(longText, Is.EqualTo("0.0754"), "Довгота не відповідає очікуваній (Mock).");
        }

        /// <summary>
        /// Тест 4.9: Перевірка помилок JavaScript.
        /// Сценарій: Завантаження сторінки та аналіз логів браузера на наявність критичних помилок.
        /// </summary>
        [Test]
        public void JavascriptErrorsTest()
        {
            // Перехід на сторінку, яка генерує помилку при завантаженні
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/javascript_error");

            // Отримуємо всі логи браузера типу "Browser"
            var logs = driver.Manage().Logs.GetLog(LogType.Browser);

            // Шукаємо лог, який містить текст специфічної помилки
            // На цій сторінці помилка зазвичай звучить як "Cannot read properties of undefined"
            bool errorFound = logs.Any(log => log.Message.Contains("Cannot read properties of undefined") || 
                                              log.Message.Contains("Cannot read property 'xyz' of undefined"));

            // Перевіряємо, що така помилка дійсно зафіксована
            Assert.That(errorFound, Is.True, "Очікувана JS помилка не знайдена в логах браузера.");
        }

        /// <summary>
        /// Тест 4.10: Перевірка функції Exit Intent.
        /// Сценарій: Емуляція руху миші та події виходу за межі вікна через JS.
        /// </summary>
        [Test]
        public void ExitIntentTest()
        {
            // Перехід на сторінку
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/exit_intent");

            // Активуємо сторінку. 
            // Іноді скрипт exit-intent не спрацьовує, якщо миша не рухалася всередині вікна.
            Actions action = new Actions(driver);
            action.MoveByOffset(100, 100).Perform();

            // Генеруємо подію mouseleave прицільно на тег <HTML>.
            // Використовуємо clientY: 0, що означає верхню межу браузера.
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            string exitScript = @"
                var event = new MouseEvent('mouseleave', {
                    'view': window,
                    'bubbles': true,
                    'cancelable': true,
                    'clientY': 0
                });
                document.documentElement.dispatchEvent(event);
            ";
            
            js.ExecuteScript(exitScript);

            // Чекаємо на модальне вікно
            var modal = driver.FindElement(By.Id("ouibounce-modal"));
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            
            // Чекаємо поки стиль display зміниться (вікно стане видимим)
            wait.Until(d => modal.Displayed);

            Assert.That(modal.Displayed, Is.True, "Модальне вікно Exit Intent не з'явилося.");
        }
    }
}