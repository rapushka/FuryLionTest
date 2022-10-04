using UnityEngine;

namespace Code.GameCycle.VictoryConditions.Conditions
{
	[CreateAssetMenu(fileName = FileName, menuName = MenuName)]
	public class ReachScoreValue : VictoryCondition
	{
		private const string FileName = "Reach Score Value X";
		private const string MenuName = "ScriptableObjects/VictoryCondition/ReachScoreValue";
	}
}