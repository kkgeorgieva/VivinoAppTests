using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;

namespace VivinoTestProject
{
    public class VivinoTests

    {
        // defining the driver, options, package, startup activity, password and email!
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions appiumOptions;
        private const string appiumUri = "http://127.0.0.1:4723/wd/hub";
        private const string VivinoAppPackage = "vivino.web.app";
        private const string VivinoStartUpActivity = "com.sphinx_solution.activities.SplashActivity";
        private const string VivinoTestAccEmail = "test_vivino@gmail.com";
        private const string VivinoTestAccountPassword = "p@ss987654321";

        [OneTimeSetUp]
        [SetUp]
        public void StartApp()
        {
            this.appiumOptions = new AppiumOptions() { PlatformName = "Android" };
            // emulator device -> appiumOptions.AddAdditionalCapability("deviceName", "5554");
            appiumOptions.AddAdditionalCapability("deviceName", "22071FDF6000SG");
            // the two things needed to start up the app
            appiumOptions.AddAdditionalCapability("appPackage", VivinoAppPackage);
            appiumOptions.AddAdditionalCapability("appActivity", VivinoStartUpActivity);
            this.driver = new AndroidDriver<AndroidElement>(new Uri(appiumUri), appiumOptions);

            //Implicit wait is used because of the slower results when loading from the internet
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(180);
            driver.ResetApp();
        }
        [OneTimeTearDown]
        public void StopApp()
        {
            driver.Quit();
        }


        [Test]
        public void ApplicationLoadedTest()
        {
            var image = driver.FindElementById("vivino.web.app:id/view_images");
            Assert.NotNull(image);

        }

        [Test]
        public void SuccessfulLoginTest()
        {
            var iHaveAnAcc = driver.FindElementById("vivino.web.app:id/txthaveaccount");
            iHaveAnAcc.Click();

            var email = driver.FindElementById("vivino.web.app:id/edtEmail");
            var password = driver.FindElementById("vivino.web.app:id/edtPassword");

            email.SendKeys(VivinoTestAccEmail);
            password.SendKeys(VivinoTestAccountPassword);

            var loginBtn = driver.FindElementById("vivino.web.app:id/action_signin");
            loginBtn.Click();

            var search = driver.FindElementById("vivino.web.app:id/wine_explorer_tab");
            Assert.NotNull(search);
        }
    }
}