using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

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
    }
}