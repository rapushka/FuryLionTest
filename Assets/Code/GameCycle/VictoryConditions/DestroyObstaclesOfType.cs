using System;
using UnityEngine;

namespace Code.GameCycle.VictoryConditions
{
	[Serializable]
	public class DestroyObstaclesOfType : VictoryCondition
	{
		[SerializeField] private ObstacleType _type;

		public ObstacleType Type => _type;
	}
}