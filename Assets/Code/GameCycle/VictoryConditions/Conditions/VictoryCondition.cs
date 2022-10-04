using UnityEngine;

namespace Code.GameCycle.VictoryConditions.Conditions
{
	public abstract class VictoryCondition : ScriptableObject
	{
		[SerializeField] private int _targetAmount;

		public int TargetAmount => _targetAmount;
	}
}