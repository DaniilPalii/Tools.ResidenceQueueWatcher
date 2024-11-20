// using OpenQA.Selenium.Edge;
// using ResidenceQueueWatcher;
//
// var defaultInterval = TimeSpan.FromMinutes(2);
// var shortInterval = TimeSpan.FromSeconds(15);
//
// const string url = "https://webqms.lublin.uw.gov.pl/Reservation";
//
// while (true)
// {
// 	using var webDriver = new EdgeDriver();
// 	var queueChecker = new QueueChecker(
// 		url,
// 		webDriver,
// 		logger: new(logDirectoryPath: "Logs"));
//
// 	TimeSpan interval;
// 	if (await queueChecker.HasFreePlace())
// 	{
// 		AlarmPlayer.Play();
// 		interval = shortInterval;
// 		// Process.Start(url);
// 	}
// 	else
// 	{
// 		interval = defaultInterval;
// 	}
//
// 	await Task.Delay(interval);
// }
