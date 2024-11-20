using System.IO;

namespace ResidenceQueueWatcher.WpfApplication;

public class Logger(string logDirectoryPath)
{
	public void Log(string message)
	{
		var dateTime = DateTime.Now;
		var formattedMessage = $"[{dateTime:yyyy-MM-dd HH:mm:ss}] {message}";

		Console.WriteLine(formattedMessage);

		Directory.CreateDirectory(logDirectoryPath);
		var logFilePath = Path.Combine(logDirectoryPath, $"{dateTime:yyyy-MM-dd}.txt");
		File.AppendAllText(logFilePath, formattedMessage + Environment.NewLine);
	}
}
