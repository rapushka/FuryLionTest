using System;
using Code.Infrastructure.Configurations.Interfaces;
using UnityEngine;

namespace Code.Infrastructure.Configurations.SerializedImplementation
{
	[Serializable]
	public class SerializedBonusConfig : IBonusesConfig
	{
		[SerializeField] private int _minValueForRocket = 5;
		[SerializeField] private int _maxValueForRocket = 7;
		[SerializeField] private int _minValueForBomb = 8;
		[SerializeField] private int _bombExplosionRange = 2;

		public int MinChainLenghtForRocket => _minValueForRocket;

		public int MaxChainLenghtForRocket => _maxValueForRocket;

		public int MinChainLenghtForBomb => _minValueForBomb;

		public int BombExplosionRange => _bombExplosionRange;
	}
}