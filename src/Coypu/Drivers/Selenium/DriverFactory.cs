using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;

namespace Coypu.Drivers.Selenium
{
    internal class DriverFactory
    {
        public IWebDriver NewWebDriver(Browser browser)  {
            if (browser == Browser.Firefox)
                return new FirefoxDriver();
            if (browser == Browser.InternetExplorer)
            {
                var options = new InternetExplorerOptions
                    {
                        IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                        EnableNativeEvents = true
                    };
                return new InternetExplorerDriver(options);
            }
            if (browser == Browser.Chrome)
                return new ChromeDriver();
            if (browser == Browser.HtmlUnit)
                return new RemoteWebDriver(DesiredCapabilities.HtmlUnit());
            if (browser == Browser.HtmlUnitWithJavaScript) {
                DesiredCapabilities desiredCapabilities = DesiredCapabilities.HtmlUnit();
                desiredCapabilities.IsJavaScriptEnabled = true;
                return new RemoteWebDriver(desiredCapabilities);
            }
            if (browser == Browser.PhantomJS)
                return new PhantomJSDriver();
            if (browser == Browser.MicrosoftEdge)
                return new EdgeDriver();
            return browserNotSupported(browser,null);
        }

        private IWebDriver browserNotSupported(Browser browser, Exception inner) {
            throw new BrowserNotSupportedException(browser, GetType(), inner);
        }
    }
}