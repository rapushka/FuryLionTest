using Code.GameLoop.Goals.TokensTypes;
using Code.Gameplay.Tokens;
using Code.UI.Windows.Panels;
using UnityEngine;

namespace Code.GameLoop.Goals.Conditions
{
	[CreateAssetMenu(fileName = "Destroy all X", menuName = "ScriptableObjects/Goal/DestroyAllObstaclesOfType")]
	public class DestroyAllObstaclesOfType : Goal
	{
		[SerializeField] private ObstacleType _type;

		public TokenUnit Type => (TokenUnit)_type;

		public override void Accept(IGoalVisitor goalVisitor) => goalVisitor.Visit(this);
	}
}