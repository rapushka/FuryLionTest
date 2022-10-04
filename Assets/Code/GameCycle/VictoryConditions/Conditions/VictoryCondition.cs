using System;
using UnityEngine;

namespace Code.GameCycle.VictoryConditions.Conditions
{
	[Serializable] 
	public class VictoryCondition
	{
		[SerializeField] private int _targetAmount;

		public int TargetAmount => _targetAmount;
	}
}