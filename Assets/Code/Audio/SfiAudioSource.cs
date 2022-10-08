using UnityEngine;

namespace Code.Audio
{
	public class SfiAudioSource
	{
		private readonly AudioSource _sfxSource;
		private readonly AudioClip _chainComposedSfx;

		public SfiAudioSource(AudioSource sfxSource, ISfxResources audios)
		{
			_sfxSource = sfxSource;
			_chainComposedSfx = audios.ChainComposed;
		}

		public void PlayChainComposed() => _sfxSource.PlayOneShot(_chainComposedSfx);
	}
}