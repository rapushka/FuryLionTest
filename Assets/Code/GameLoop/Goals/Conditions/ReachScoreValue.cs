using UnityEngine;

namespace Code.GameLoop.Goals.Conditions
{
	[CreateAssetMenu(fileName = "Score N", menuName = "ScriptableObjects/Goal/ReachScoreValue")]
	public class ReachScoreValue : Goal
	{
		[SerializeField] private int _targetScoreValue;

		public int TargetScoreValue => _targetScoreValue;
	}
}