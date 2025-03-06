using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace ResidenceQueueWatcher.ConsoleApplication;

public class WebDriverFactory
{
	public IWebDriver CreateNext()
	{
		var webDriver = NextBrowser switch
		{
			Browser.Edge => CreateEdgeDriver(),
			Browser.Chrome => CreateChromeDriver(),
			Browser.Firefox => CreateFirefoxDriver(),
			_ => throw new ArgumentOutOfRangeException(),
		};

		var timeouts = webDriver.Manage().Timeouts();
		timeouts.ImplicitWait = TimeSpan.FromSeconds(1);
		timeouts.PageLoad = TimeSpan.FromSeconds(3);

		return webDriver;
	}

	private static IWebDriver CreateEdgeDriver()
	{
		var service = EdgeDriverService.CreateDefaultService();
		service.HideCommandPromptWindow = true;

		var options = new EdgeOptions();
		// options.BinaryLocation = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
		options.AddArgument("start-maximized");
		// options.AddArgument("no-sandbox");
		// options.AddArgument(@"user-data-dir=C:\Users\Daniil\AppData\Local\Microsoft\Edge\User Data");
		options.AddExcludedArgument("enable-automation");

		options.AddAdditionalEdgeOption("useAutomationExtension", false);

		var webDriver = new EdgeDriver(service, options);

		// webDriver.ExecuteCdpCommand(
		// 	"Network.setUserAgentOverride",
		// 	new()
		// 	{
		// 		["userAgent"]
		// 			= "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36",
		// 	});
		return webDriver;
	}

	private static IWebDriver CreateChromeDriver()
	{
		var service = ChromeDriverService.CreateDefaultService();
		service.HideCommandPromptWindow = true;

		var options = new ChromeOptions();
		options.AddArgument("start-maximized");
		options.AddExcludedArgument("enable-automation");

		options.AddAdditionalChromeOption("useAutomationExtension", false);

		var webDriver = new ChromeDriver(service, options);

		return webDriver;
	}

	private static IWebDriver CreateFirefoxDriver()
	{
		var service = FirefoxDriverService.CreateDefaultService();
		service.HideCommandPromptWindow = true;

		var options = new FirefoxOptions();
		options.AddArgument("start-maximized");

		var webDriver = new FirefoxDriver(service, options);

		return webDriver;
	}

	private Browser NextBrowser
	{
		get
		{
			var browser = nextBrowser++;

			if (nextBrowser > Browser.Firefox)
				nextBrowser = Browser.Edge;

			return browser;
		}
	}

	private Browser nextBrowser = Browser.Edge;

	private enum Browser { Edge, Chrome, Firefox }
}
