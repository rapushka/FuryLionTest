using UnityEngine;

namespace Code.Audio
{
	public class MusicAudioSource
	{
		public MusicAudioSource(AudioSource musicSource, AudioClip audiosMusic)
		{
			musicSource.clip = audiosMusic;
		}
	}
}