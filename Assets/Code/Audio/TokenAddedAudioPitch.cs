using UnityEngine;

namespace Code.Audio
{
	public class TokenAddedAudioPitch
	{
		private readonly AudioSource _tokenAddedSfxSource;
		private readonly float _pitchStep;
		private readonly float _initialPitch;

		private float _pitch;

		public TokenAddedAudioPitch(AudioSource tokenAddedSfxSource)
		{
			_tokenAddedSfxSource = tokenAddedSfxSource;

			_pitchStep = 0.05f;
			_initialPitch = _tokenAddedSfxSource.pitch;
			ResetPitch();
		}

		public void IncreasePitch()
		{
			_pitch += _pitchStep;
			_tokenAddedSfxSource.pitch = _pitch;
		}

		public void DecreasePitch()
		{
			_pitch -= _pitchStep;
			_tokenAddedSfxSource.pitch = _pitch;
		}

		public void ResetPitch()
		{
			_pitch = _initialPitch;
			_tokenAddedSfxSource.pitch = _pitch;
		}
	}
}