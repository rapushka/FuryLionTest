using System;
using UnityEngine;

namespace Code.GameCycle.VictoryConditions
{
	[Serializable] 
	public abstract class VictoryCondition
	{
		[SerializeField] private int _targetAmount;

		public int TargetAmount => _targetAmount;
	}
}