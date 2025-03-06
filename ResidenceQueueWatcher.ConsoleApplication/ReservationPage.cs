using OpenQA.Selenium;

namespace ResidenceQueueWatcher.ConsoleApplication;

public sealed class ReservationPage(IWebDriver webDriver)
{
	public async Task LoadAsync()
	{
		await webDriver.Navigate().GoToUrlAsync("https://webqms.lublin.uw.gov.pl/Reservation");
	}

	public async Task ClickFirstStepButtonAsync()
	{
		var randomElement = random.Next(1, 2) switch
		{
			1 => webDriver.FindElement(By.XPath("//*[@id=\"app\"]/div[2]/div[1]/div/div[1]/div/div[1]/div[2]/div/h5")),
			_ => webDriver.FindElement(By.XPath("/html/body/div[2]/div[3]/div[1]/div")),
		};

		await WaitAsync();
		ClickElementUsingJavaScript(randomElement);

		await WaitAsync();
		webDriver.FindElement(By.XPath("//*[@id=\"form-wizard\"]/div[2]/ul/li[3]")).SendKeys(Keys.Control);

		await WaitAsync();
		FirstStepButton.Click();
	}

	public async Task ClickSecondStepButtonAsync()
	{
		var randomElement = random.Next(1, 2) switch
		{
			1 => webDriver.FindElement(By.XPath("//*[@id=\"form-wizard\"]/div[2]/ul/li[3]")),
			_ => webDriver.FindElement(By.XPath("//*[@id=\"Operacja2\"]/div[1]")),
		};

		await WaitAsync();
		ClickElementUsingJavaScript(randomElement);

		await WaitAsync();
		webDriver.FindElement(By.XPath("//*[@id=\"form-wizard\"]/div[2]/ul/li[2]")).SendKeys(Keys.Control);

		var button = random.Next(1, 2) switch
		{
			1 => NextButton,
			_ => SecondStepButton,
		};

		await WaitAsync();
		button.Click();
	}

	public string GetReservationInfo()
	{
		return ReservationInfoLabel.Text.Trim();
	}

	private Task WaitAsync()
	{
		return Task.Delay(
			TimeSpan.FromMilliseconds(random.Next(336, 4768)));
	}

	private void ClickElementUsingJavaScript(IWebElement element)
	{
		IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor) webDriver;
		jsExecutor.ExecuteScript("arguments[0].click();", element);
	}

	private IWebElement ReservationInfoLabel
		=> webDriver.FindElement(By.CssSelector(".wizard-tab-content h5"));

	private IWebElement FirstStepButton
		=> webDriver.FindElement(By.XPath("//*[@id=\"form-wizard\"]/div[2]/ul/li[1]/a"));

	private IWebElement SecondStepButton
		=> webDriver.FindElement(By.XPath("//*[@id=\"form-wizard\"]/div[2]/ul/li[2]/a"));

	private IWebElement NextButton
		=> webDriver.FindElement(By.XPath("//*[@id=\"form-wizard\"]/div[3]/div/div/div/button"));

	private readonly Random random = new();
}
