using OpenQA.Selenium.Edge;
using ResidenceQueueWatcher;

Console.WriteLine("Start");

QueueChecker.Result result;
do
{
	using var queueChecker = new QueueChecker(
		url: "https://webqms.lublin.uw.gov.pl/Reservation",
		webDriver: new EdgeDriver(),
		logger: new(logDirectoryPath: "Logs"));

	result = await queueChecker.CheckAsync();

	await Task.Delay(TimeSpan.FromSeconds(60));
}
while (true);

Console.WriteLine("Stop");
