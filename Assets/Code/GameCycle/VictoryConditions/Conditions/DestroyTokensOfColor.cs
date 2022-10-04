using Code.GameCycle.VictoryConditions.TokensTypes;
using Code.Gameplay.Tokens;
using UnityEngine;

namespace Code.GameCycle.VictoryConditions.Conditions
{
	[CreateAssetMenu(fileName = "N COLOR Tokens", menuName = "ScriptableObjects/Goal/DestroyTokensOfColor")]
	public class DestroyTokensOfColor : Goal
	{
		[SerializeField] private TokenColor _targetColor;
		[SerializeField] private int _targetCount;

		public TokenUnit Color => (TokenUnit)_targetColor;

		public int TargetCount => _targetCount;
	}
}