using Code.GameCycle.VictoryConditions;
using Code.Gameplay.Tokens;
using Code.Levels.LevelGeneration.LeverEditor;
using UnityEngine;

namespace Code.Levels
{
	[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 0)]
	public class Level : ScriptableObject
	{
		[SerializeField] private int _actionsCount;
		[SerializeField] private VictoryConditionsCollection _victoryConditions;
		[SerializeField] private ArrayLayout<TokenUnit> _tokens;

		public int ActionCount => _actionsCount;
		
		public TokenUnit[,] TokenTypesArray => _tokens.ToRectangularArray();

		private void OnValidate() => _actionsCount = _actionsCount > 0 ? _actionsCount : 1;
	}
}