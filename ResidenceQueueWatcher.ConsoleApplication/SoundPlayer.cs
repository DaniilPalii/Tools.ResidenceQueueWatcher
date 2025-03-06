using System.Diagnostics;

namespace ResidenceQueueWatcher.ConsoleApplication;

public static class SoundPlayer
{
	public static void PlaySuccess()
	{
		ShellExecute(@"C:\Windows\Media\Ring08.wav");
	}

	public static void PlayError()
	{
		ShellExecute(@"C:\Windows\Media\Alarm10.wav");
	}

	private static void ShellExecute(string fileName)
	{
		var process = new Process();
		process.StartInfo = new(fileName)
		{
			UseShellExecute = true,
		};

		process.Start();
	}
}
