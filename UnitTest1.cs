using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V102.Network;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace LocatorsAndChromeDevToolsHometask
{
    public class Tests
    {
        private IWebDriver _driverChrome;

        private readonly By _loginField = By.ClassName("cvxzFsio");
        private readonly By _passwordField = By.Name("password");
        private readonly By _continueButton = By.CssSelector("button.Ol0-ktls");
        private readonly By _toWriteLetter = By.XPath("//button[@class='button primary compose']");
        private readonly By _sandButton = By.XPath("//button[@class='button primary send']");
        private readonly By _emailField = By.Name("toFieldInput");



        private const string _username = "TestEpam1";
        private const string _password = "qwe!@#qwe";
        private const string _emailToSend = "vtrbullet@gmail.com";
        private const string _subjectEmailToSend = "Test";
        private const string _testMessenge = "Hello\nIt's test letter";



        [SetUp]
        public void Setup()
        {
            _driverChrome = new ChromeDriver();
            _driverChrome.Navigate().GoToUrl("https://accounts.ukr.net/login");
            _driverChrome.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            var login = _driverChrome.FindElement(_loginField);
            login.SendKeys(_username);

            var password = _driverChrome.FindElement(_passwordField);
            password.SendKeys(_password);

            _driverChrome.FindElement(_continueButton).Click();

            WebDriverWait waitForLoggin = new WebDriverWait(_driverChrome, TimeSpan.FromSeconds(10));
            var newLetter = waitForLoggin.Until(drv => drv.FindElement(_toWriteLetter));
            newLetter.Click();

            WebDriverWait waitForInput = new WebDriverWait(_driverChrome, TimeSpan.FromSeconds(10));
            var inputEmail = waitForInput.Until(drv => drv.FindElement(_emailField));
            inputEmail.SendKeys(_emailToSend);

            _driverChrome.FindElement(By.Name("subject")).SendKeys(_subjectEmailToSend);
            _driverChrome.FindElement(By.Id("mce_0_ifr")).SendKeys(_testMessenge);
            _driverChrome.FindElement(_sandButton).Click();

            WebDriverWait waitForSending = new WebDriverWait(_driverChrome, TimeSpan.FromSeconds(10));
            var sending = waitForSending.Until(drv => drv.FindElement(By.XPath("//div[@class='sendmsg__ads-ready']")));

            Assert.True(sending.Enabled);

        }

        [TearDown]
         public void TearDown()
         {
             _driverChrome.Quit();
         }
    }
}