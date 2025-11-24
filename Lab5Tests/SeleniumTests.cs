using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;

namespace Lab5Tests
{
    /// <summary>
    /// Базовий тестовий клас, що містить методи налаштування драйвера.
    /// </summary>
    public class SeleniumTests
    {
        protected IWebDriver driver;

        /// <summary>
        /// Ініціалізація драйвера Chrome з налаштуваннями геолокації перед кожним тестом.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();

            // Автоматичний дозвіл на використання геолокації
            options.AddUserProfilePreference("profile.default_content_setting_values.geolocation", 1);
            options.AddArgument("--disable-notifications");

            driver = new ChromeDriver(options);

            // Параметри для емуляції геолокації (Лондон)
            var coordinates = new Dictionary<string, object>
            {
                { "latitude", 51.5055 },
                { "longitude", 0.0754 },
                { "accuracy", 1 }
            };

            // Використання CDP команд для встановлення фіктивних координат
            ((ChromeDriver)driver).ExecuteCdpCommand("Emulation.setGeolocationOverride", coordinates);

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        /// <summary>
        /// Закриття браузера та звільнення ресурсів після виконання тесту.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}