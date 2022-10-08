using UnityEngine;

namespace Code.Audio
{
	public class SfiAudioSource
	{
		private readonly AudioSource _sfxSource;
		private readonly AudioClip _chainComposedSfx;
		private readonly AudioClip _bonusSpawnedSfx;
		private readonly AudioClip _tokenRemovedFromChainSfx;

		public SfiAudioSource(AudioSource sfxSource, ISfxResources audios)
		{
			_sfxSource = sfxSource;
			_chainComposedSfx = audios.ChainComposed;
			_bonusSpawnedSfx = audios.BonusSpawned;
			_tokenRemovedFromChainSfx = audios.TokenRemovedFromChain;
		}

		public void PlayChainComposed() => Play(_chainComposedSfx);

		public void PlayBonusSpawned() => Play(_bonusSpawnedSfx);

		public void PlayTokenRemoved() => Play(_tokenRemovedFromChainSfx);

		private void Play(AudioClip clip) => _sfxSource.PlayOneShot(clip);
	}
}