using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace ResidenceQueueWatcher;

public sealed class QueueChecker(string url, IWebDriver webDriver, Logger logger) : IDisposable
{
	public async Task<Result> CheckAsync()
	{
		try
		{
			var options = webDriver.Manage();
			options.Window.Size = new(width: 1112, height: 1100);
			var timeouts = options.Timeouts();
			timeouts.ImplicitWait = TimeSpan.FromSeconds(5);
			timeouts.PageLoad = TimeSpan.FromSeconds(5);

			await webDriver.Navigate().GoToUrlAsync(url);

			await Task.Delay(500);
			var applyForResidenceButton = webDriver.FindElement(applyForResidenceButtonSelector);
			applyForResidenceButton.Click();

			await Task.Delay(500);
			new Actions(webDriver).ScrollByAmount(deltaX: 0, deltaY: int.MaxValue).Perform();
			await Task.Delay(100);
			var nextButton = webDriver.FindElement(nextButtonSelector);
			nextButton.Click();

			var reservationInfoLabel = webDriver.FindElement(reservationInfoLabelSelector);
			var reservationInfo = reservationInfoLabel.Text;
			logger.Log(reservationInfo);

			webDriver.Close();

			return reservationInfo.Contains("Rezerwacje dostępne", StringComparison.CurrentCultureIgnoreCase)
				? Result.HasFreePlace
				: Result.NoFreePlace;
		}
		catch (Exception exception)
		{
			logger.Log(exception.Message);
			return Result.UnhandledBehavior;
		}
	}

	public void Dispose()
	{
		webDriver.Dispose();
	}

	public enum Result
	{
		HasFreePlace,
		NoFreePlace,
		CaptchaRequired,
		UnhandledBehavior,
	}

	private readonly By applyForResidenceButtonSelector = By.CssSelector(".row:nth-child(2) > .mb-2 .btn");
	private readonly By nextButtonSelector = By.CssSelector(".footer-btn");
	private readonly By reservationInfoLabelSelector = By.CssSelector(".wizard-tab-content h5");
}
