using UnityEngine;

namespace Code.Audio
{
	public class SfxAudioSource
	{
		private readonly AudioSource _sfxSource;
		private readonly AudioClip _chainComposedSfx;
		private readonly AudioClip _bonusSpawnedSfx;
		private readonly AudioClip _tokenRemovedFromChainSfx;
		private readonly AudioClip _goalCompleted;

		public SfxAudioSource(AudioSource sfxSource, ISfxResources audios)
		{
			_sfxSource = sfxSource;
			_chainComposedSfx = audios.ChainComposed;
			_bonusSpawnedSfx = audios.BonusSpawned;
			_tokenRemovedFromChainSfx = audios.TokenRemovedFromChain;
			_goalCompleted = audios.GoalCompleted;
		}

		public void PlayChainComposed() => Play(_chainComposedSfx);

		public void PlayBonusSpawned() => Play(_bonusSpawnedSfx);

		public void PlayTokenRemoved() => Play(_tokenRemovedFromChainSfx);
		
		public void PlayGoalCompleted() => Play(_goalCompleted);

		private void Play(AudioClip clip) => _sfxSource.PlayOneShot(clip);
	}
}