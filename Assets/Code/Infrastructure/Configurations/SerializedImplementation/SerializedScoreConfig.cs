using System;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;

namespace Code.Infrastructure.Configurations.SerializedImplementation
{
	[Serializable]
	public class SerializedScoreConfig : IScoreConfig
	{
		[field: SerializeField] public int ScoreMultiplier { get; private set; } = 150;

		[field: SerializeField] public float MultiplierPerTokenInChain { get; private set; } = 1.2f;
	}
}