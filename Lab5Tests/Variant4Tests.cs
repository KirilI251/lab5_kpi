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
    }
}