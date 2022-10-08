using UnityEngine;

namespace Code.Audio
{
	[CreateAssetMenu(fileName = "Audio Collection", menuName = "ScriptableObjects/Audio Collection")]
	public class AudioCollection : ScriptableObject
	{
		[SerializeField] private AudioClip _music;
		[SerializeField] private AudioClip _tokenAddedToChainSfx;
		[SerializeField] private AudioClip _chainComposedSfx;

		public AudioClip Music => _music;

		public AudioClip TokenAddedToChainSfx => _tokenAddedToChainSfx;

		public AudioClip ChainComposedSfx => _chainComposedSfx;
	}
}