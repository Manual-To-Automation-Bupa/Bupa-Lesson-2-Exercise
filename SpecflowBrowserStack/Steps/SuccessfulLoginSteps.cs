using TechTalk.SpecFlow;
using FluentAssertions;
using SpecflowBrowserStack.Drivers;
using OpenQA.Selenium;

namespace SpecflowBrowserStack.Steps
{
    [Binding]
    public class SuccessfulLoginSteps
    {
        private readonly WebDriver _webDriver;
        private static bool passed = false;

        public SuccessfulLoginSteps(WebDriver driver)
        {
            _webDriver = driver;
        }

        [Given(@"I am on ""(.*)""")]
        public void GivenINavigatedToGoogle(string url)
        {
            _webDriver.Current.Navigate().GoToUrl(url);
        }

        [When(@"I login with the correct username ""(.*)"" using the correct password ""(.*)""")]
        public void WhenILoginCorrectly(string username, string password)
        {
            IWebElement usernameField = _webDriver.Current.FindElement(By.CssSelector("#username"));
            usernameField.SendKeys(username);

            IWebElement passwordField = _webDriver.Current.FindElement(By.CssSelector("#password"));
            passwordField.SendKeys(password);

            IWebElement submitButton = _webDriver.Current.FindElement(By.CssSelector("#submit"));
            submitButton.Click();
        }

        [Then(@"I should be successfully logged in")]
        public void ThenTheResultShouldBeOnTheScreen()
        {
            bool isDisplayed = _webDriver.Wait.Until(heading => heading.FindElement(By.CssSelector(".post-title")).Displayed);
            isDisplayed.Should().BeTrue();

            if (isDisplayed)
            {
                passed = true;
            }

            if (passed)
            {
                ((IJavaScriptExecutor)_webDriver.Current).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \"The login was successful\"}}");
            }
            else
            {
                ((IJavaScriptExecutor)_webDriver.Current).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \"The login was not successful\"}}");
            }

        }
    }
}
