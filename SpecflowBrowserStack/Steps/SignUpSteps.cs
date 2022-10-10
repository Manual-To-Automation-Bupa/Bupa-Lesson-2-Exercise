using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SpecflowBrowserStack.Drivers;
using System;
using TechTalk.SpecFlow;
 
 namespace SpecflowBrowserStack.Steps
{
     [Binding]
     public class SignUpSteps
     {
        private readonly WebDriver _webDriver;
        private string emailAddress = "test" + GetRandomNumber() + "@testemail" + GetRandomNumber() + ".com";
        readonly DefaultWait<IWebDriver> fluentWait;

        public SignUpSteps(WebDriver driver)
        {
            _webDriver = driver;
            fluentWait = new DefaultWait<IWebDriver>(_webDriver.Current);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
            

        }
        private static int GetRandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(10000, 99999);
        }

        [Given(@"I go to ""(.*)""")]
        public void GivenIGoTo(string url)
        {
            _webDriver.Current.Navigate().GoToUrl(url);
        }
         
        [When(@"I click on Sign Up / Login")]
        public void WhenIClickOnSignUpLogin()
        {
            IWebElement signUpElement = _webDriver.Current.FindElement(By.XPath("//a[@href='/login']"));
            signUpElement.Click();
        }
         
        [When(@"I enter a name and email address into the sign up section")]
        public void WhenIEnterANameAndEmailAddressIntoTheSignUpSection()
        {
            IWebElement nameField = fluentWait.Until(x => x.FindElement(By.XPath("//input[@data-qa='signup-name']")));
            nameField.SendKeys("FirstName LastName");

            IWebElement emailAddressField = _webDriver.Current.FindElement(By.XPath("//input[@data-qa='signup-email']"));
            emailAddressField.SendKeys(emailAddress);

            IWebElement signUpButton = _webDriver.Current.FindElement(By.XPath("//button[@data-qa='signup-button']"));
            signUpButton.Click();
        }
         
        [When(@"I fill in my personal details")]
        public void WhenIFillInMyPersonalDetails()
        {
            IWebElement genderSelectField = fluentWait.Until(x => x.FindElement(By.Id("id_gender1")));
            genderSelectField.Click();

            IWebElement passwordField = _webDriver.Current.FindElement(By.Id("password"));
            passwordField.SendKeys("Password123!");

            SelectElement daysDropdown = new SelectElement(_webDriver.Current.FindElement(By.Id("days")));
            daysDropdown.SelectByValue("1");

            SelectElement monthsDropdown = new SelectElement(_webDriver.Current.FindElement(By.Id("months")));
            monthsDropdown.SelectByValue("1");

            SelectElement yearsDropdown = new SelectElement(_webDriver.Current.FindElement(By.Id("years")));
            yearsDropdown.SelectByValue("1990");

            IWebElement firstNameField = _webDriver.Current.FindElement(By.Id("first_name"));
            firstNameField.SendKeys("FirstName");

            IWebElement lastNameField = _webDriver.Current.FindElement(By.Id("last_name"));
            lastNameField.SendKeys("LastName");

            IWebElement addressLine1Field = _webDriver.Current.FindElement(By.Id("address1"));
            addressLine1Field.SendKeys("1 Main Street");

            IWebElement stateField = _webDriver.Current.FindElement(By.Id("state"));
            stateField.SendKeys("State");

            IWebElement cityField = _webDriver.Current.FindElement(By.Id("city"));
            cityField.SendKeys("City");

            IWebElement zipCodeElement = _webDriver.Current.FindElement(By.Id("zipcode"));
            zipCodeElement.SendKeys("12345");

            IWebElement mobileNumberElement = _webDriver.Current.FindElement(By.Id("mobile_number"));
            mobileNumberElement.SendKeys("1234567890");

            IWebElement createAccountButton = fluentWait.Until(x => x.FindElement(By.XPath("//button[@data-qa='create-account']")));
            createAccountButton.Click();
        }
         
        [Then(@"My new account was successfully created")]
        public void ThenMyNewAccountWasSuccessfullyCreated()
        {
            IWebElement continueButton = fluentWait.Until(x => x.FindElement(By.XPath("//a[@data-qa='continue-button']")));
            continueButton.Displayed.Should().BeTrue();

            continueButton.Click();

            IWebElement logoutElement = fluentWait.Until(x => x.FindElement(By.XPath("//a[@href='/logout']")));
            logoutElement.Displayed.Should().BeTrue();
        }
     }
 }