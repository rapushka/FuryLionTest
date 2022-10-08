using UnityEngine;

namespace Code.Audio
{
	[CreateAssetMenu(fileName = "Audio Collection", menuName = "ScriptableObjects/Audio Collection")]
	public class AudioCollection : ScriptableObject, ISfxResources
	{
		[SerializeField] private AudioClip _music;
		[SerializeField] private AudioClip _tokenAddedToChain;
		[SerializeField] private float _tokenAddedToChainSfxPitchStep = 0.05f;
		[SerializeField] private AudioClip _chainComposed;
		[SerializeField] private AudioClip _bonusSpawned;
		[SerializeField] private AudioClip _tokenRemovedFromChain;

		public AudioClip Music => _music;
		
		public AudioClip TokenAddedToChain => _tokenAddedToChain;

		public float TokenAddedToChainSfxPitchStep => _tokenAddedToChainSfxPitchStep;

		public AudioClip ChainComposed => _chainComposed;

		public AudioClip BonusSpawned => _bonusSpawned;

		public AudioClip TokenRemovedFromChain => _tokenRemovedFromChain;
	}
}