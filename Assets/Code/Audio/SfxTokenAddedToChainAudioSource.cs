using UnityEngine;

namespace Code.Audio
{
	public class SfxTokenAddedToChainAudioSource
	{
		private readonly AudioSource _sfxSource;
		private readonly float _pitchStep;
		private readonly float _initialPitch;

		private float _pitch;

		public SfxTokenAddedToChainAudioSource(AudioSource sfxSource)
		{
			_sfxSource = sfxSource;

			_pitchStep = 0.05f;
			_initialPitch = _sfxSource.pitch;
			ResetPitch();
		}

		public void PlayTokenAddedToChain()
		{
			_sfxSource.PlayOneShot(_sfxSource.clip);
			_pitch += _pitchStep;
			_sfxSource.pitch = _pitch;
		}

		public void ResetPitch()
		{
			_pitch = _initialPitch;
			_sfxSource.pitch = _pitch;
		}

		public void DecreasePitch()
		{
			_pitch -= _pitchStep;
			_sfxSource.pitch = _pitch;
		}
	}
}