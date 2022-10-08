using UnityEngine;

namespace Code.Audio
{
	[CreateAssetMenu(fileName = "Audio Collection", menuName = "ScriptableObjects/Audio Collection")]
	public class AudioCollection : ScriptableObject
	{
		[SerializeField] private AudioClip _music;
		[SerializeField] private AudioClip _tokenAddedToChainSfx;
		[SerializeField] private AudioClip _chainComposedSfx;
		[SerializeField] private float _tokenAddedToChainSfxPitchStep = 0.05f;

		public AudioClip Music => _music;

		public AudioClip TokenAddedToChainSfx => _tokenAddedToChainSfx;

		public AudioClip ChainComposedSfx => _chainComposedSfx;

		public float TokenAddedToChainSfxPitchStep => _tokenAddedToChainSfxPitchStep;
	}
}