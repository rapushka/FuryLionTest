using UnityEngine;

namespace Code.Audio
{
	public class TokenAddedAudioPitch
	{
		private readonly AudioSource _tokenAddedSfxSource;
		private readonly float _pitchStep;
		private readonly float _initialPitch;

		public TokenAddedAudioPitch(AudioSource tokenAddedSfxSource, float pitchStep)
		{
			_tokenAddedSfxSource = tokenAddedSfxSource;
			_pitchStep = pitchStep;

			_initialPitch = Pitch;
			ResetPitch();
		}

		private float Pitch
		{
			get => _tokenAddedSfxSource.pitch;
			set => _tokenAddedSfxSource.pitch = value;
		}

		public void IncreasePitch() => Pitch += _pitchStep;

		public void DecreasePitch() => Pitch -= _pitchStep;

		public void ResetPitch() => Pitch = _initialPitch;
	}
}