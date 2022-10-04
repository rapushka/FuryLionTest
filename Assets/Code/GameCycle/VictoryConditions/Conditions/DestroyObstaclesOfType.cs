using Code.GameCycle.VictoryConditions.TokensTypes;
using UnityEngine;

namespace Code.GameCycle.VictoryConditions.Conditions
{
	[CreateAssetMenu(fileName = FileName, menuName = MenuName)]
	public class DestroyObstaclesOfType : VictoryCondition
	{
		[SerializeField] private ObstacleType _type;

		private const string FileName = "Destroy X Obstacles Of Type X";
		private const string MenuName = "ScriptableObjects/VictoryCondition/DestroyObstaclesOfType";
		
		public ObstacleType Type => _type;
	}
}