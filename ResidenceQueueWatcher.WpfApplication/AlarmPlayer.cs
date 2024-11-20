using System.Media;

namespace ResidenceQueueWatcher.WpfApplication;

public static class AlarmPlayer
{
	public static void Play()
	{
		var player = new SoundPlayer { SoundLocation = @"C:\Windows\Media\Ring08.wav" };
		player.Play();
	}
}
