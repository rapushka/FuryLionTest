using System;
using Code.GameCycle.VictoryConditions.TokensTypes;
using UnityEngine;

namespace Code.GameCycle.VictoryConditions.Conditions
{
	[Serializable]
	public class DestroyObstaclesOfType : VictoryCondition
	{
		[SerializeField] private ObstacleType _type;

		public ObstacleType Type => _type;
	}
}