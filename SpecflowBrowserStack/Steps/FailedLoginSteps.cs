using TechTalk.SpecFlow;
using FluentAssertions;
using SpecflowBrowserStack.Drivers;
using OpenQA.Selenium;

namespace SpecflowBrowserStack.Steps
{
    [Binding]
    public class FailedLoginSteps
    {
        private readonly WebDriver _webDriver;
        private static bool passed = false;

        public FailedLoginSteps(WebDriver driver)
        {
            _webDriver = driver;
        }

        [Given(@"I navigate to the ""(.*)"" page")]
        public void GivenINavigatedToGoogle(string url)
        {
            _webDriver.Current.Navigate().GoToUrl(url);
        }

        [When(@"I login with incorrect details")]
        public void WhenILoginCorrectly()
        {
            IWebElement usernameField = _webDriver.Current.FindElement(By.CssSelector("#username"));
            usernameField.SendKeys("wronguser");

            IWebElement passwordField = _webDriver.Current.FindElement(By.CssSelector("#password"));
            passwordField.SendKeys("Password123");

            IWebElement submitButton = _webDriver.Current.FindElement(By.CssSelector("#submit"));
            submitButton.Click();
        }

        [Then(@"The login is failed")]
        public void ThenTheResultShouldBeOnTheScreen()
        {
            bool isDisplayed = _webDriver.Wait.Until(errorMessage => errorMessage.FindElement(By.CssSelector("#error")).Displayed);
            isDisplayed.Should().BeTrue();

            if (isDisplayed)
            {
                passed = true;
            }

            if (passed)
            {
                ((IJavaScriptExecutor)_webDriver.Current).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \"The login failed as expected\"}}");
            }
            else
            {
                ((IJavaScriptExecutor)_webDriver.Current).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"The login failure message did not display\"}}");
            }

        }
    }
}
