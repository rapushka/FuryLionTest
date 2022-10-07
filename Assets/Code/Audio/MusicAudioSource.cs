using UnityEngine;

namespace Code.Audio
{
	public class MusicAudioSource
	{
		private readonly AudioSource _musicSource;

		public MusicAudioSource(AudioSource musicSource)
		{
			_musicSource = musicSource;
		}
	}
}