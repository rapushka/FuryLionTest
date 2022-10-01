using System;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;

namespace Code.Infrastructure.Configurations.SerializedImplementation
{
	[Serializable]
	public class SerializedScoreConfig : IScoreConfig
	{
		[SerializeField] private int _scoreMultiplier = 150;
		[SerializeField] private float _multiplierPerTokenInChain = 1.2f;

		public int ScoreMultiplier => _scoreMultiplier;
		public float MultiplierPerTokenInChain => _multiplierPerTokenInChain;
	}
}