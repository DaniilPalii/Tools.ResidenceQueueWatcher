using ResidenceQueueWatcher.ConsoleApplication;

var logger = new Logger(logDirectoryPath: "Logs");

var webDriverFactory = new WebDriverFactory();

var webDriver = webDriverFactory.CreateNext();
var page = new ReservationPage(webDriver);
await page.LoadAsync();

Console.Write("Navigate to operation page end type interval in seconds to continue: ");
var intervalInput = Console.ReadLine();
var defaultInterval = TimeSpan.FromSeconds(int.TryParse(intervalInput!, out var x) ? x : 20);
Console.WriteLine($"OK, interval is {defaultInterval}");;
Console.WriteLine("OK");

var succeedAttemptInterval = TimeSpan.FromMinutes(5);

while (true)
{
	TimeSpan? interval = null;

	try
	{
		await page.ClickFirstStepButtonAsync();
		await page.ClickSecondStepButtonAsync();
	}
	catch (Exception exception)
	{
		logger.Log(exception.Message);

		webDriver.Close();
		webDriver.Dispose();

		webDriver = webDriverFactory.CreateNext();
		page = new(webDriver);
		await page.LoadAsync();

		SoundPlayer.PlayError();
		Console.Write("Navigate to operation page and press enter to continue");
		Console.ReadLine();
		Console.WriteLine("OK");
	}

	var reservationInfo = page.GetReservationInfo();
	logger.Log(reservationInfo);

	if (reservationInfo.Contains("Rezerwacje dostępne", StringComparison.CurrentCultureIgnoreCase))
	{
		SoundPlayer.PlaySuccess();
		interval = succeedAttemptInterval;
	}

	await Task.Delay(interval ?? defaultInterval);
}

webDriver.Close();
webDriver.Dispose();
