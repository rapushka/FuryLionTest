using UnityEngine;

namespace Code.Audio
{
	public class TokenAddedAudioSource
	{
		private readonly AudioSource _sfxSource;
		private readonly AudioClip _tokenAddedSfx;

		public TokenAddedAudioSource(AudioSource sfxSource, AudioClip tokenAddedSfx)
		{
			_sfxSource = sfxSource;
			_tokenAddedSfx = tokenAddedSfx;
		}

		public void Play() => _sfxSource.PlayOneShot(_tokenAddedSfx);
	}
}