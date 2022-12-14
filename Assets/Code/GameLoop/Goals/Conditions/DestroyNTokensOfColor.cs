using Code.GameLoop.Goals.TokensTypes;
using Code.Gameplay.Tokens;
using Code.UI.Windows.Panels;
using UnityEngine;

namespace Code.GameLoop.Goals.Conditions
{
	[CreateAssetMenu(fileName = "N COLOR Tokens", menuName = "ScriptableObjects/Goal/DestroyNTokensOfColor")]
	public class DestroyNTokensOfColor : Goal
	{
		[SerializeField] private TokenColor _targetColor;
		[SerializeField] private int _targetCount;

		public TokenUnit Color => (TokenUnit)_targetColor;

		public int TargetCount => _targetCount;

		public override void Accept(IGoalVisitor goalVisitor) => goalVisitor.Visit(this);
	}
}