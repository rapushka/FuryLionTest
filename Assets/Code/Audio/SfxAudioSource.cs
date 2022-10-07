using UnityEngine;

namespace Code.Audio
{
	public class SfxAudioSource
	{
		private readonly AudioSource _sfxSource;

		public SfxAudioSource(AudioSource sfxSource)
		{
			_sfxSource = sfxSource;
		}

		public void PlayTokenAddedToChain()
		{
			_sfxSource.PlayOneShot(_sfxSource.clip);
		}
	}
}