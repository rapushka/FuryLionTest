using Code.GameCycle.Goals.TokensTypes;
using Code.Gameplay.Tokens;
using UnityEngine;

namespace Code.GameCycle.Goals.Conditions
{
	[CreateAssetMenu(fileName = "Destroy all X", menuName = "ScriptableObjects/Goal/DestroyAllObstaclesOfType")]
	public class DestroyAllObstaclesOfType : Goal
	{
		[SerializeField] private ObstacleType _type;

		public TokenUnit Type => (TokenUnit)_type;
	}
}