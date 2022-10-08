using UnityEngine;

namespace Code.Audio
{
	[CreateAssetMenu(fileName = "Audio Collection", menuName = "ScriptableObjects/Audio Collection")]
	public class AudioCollection : ScriptableObject, ISfxResources
	{
		[SerializeField] private AudioClip _music;
		[SerializeField] private AudioClip _tokenAddedToChainSfx;
		[SerializeField] private AudioClip _chainComposed;
		[SerializeField] private float _tokenAddedToChainSfxPitchStep = 0.05f;

		public AudioClip Music => _music;

		public AudioClip TokenAddedToChainSfx => _tokenAddedToChainSfx;

		public AudioClip ChainComposed => _chainComposed;

		public float TokenAddedToChainSfxPitchStep => _tokenAddedToChainSfxPitchStep;
	}
}