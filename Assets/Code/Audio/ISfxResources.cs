using UnityEngine;

namespace Code.Audio
{
	public interface ISfxResources
	{
		AudioClip ChainComposed { get; }
		
		AudioClip BonusSpawned { get; }
		
		AudioClip TokenRemovedFromChain { get; }
		
		AudioClip GoalCompleted { get; }
	}
}