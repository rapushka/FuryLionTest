using System.Collections.Generic;
using Code.GameCycle.VictoryConditions.Conditions;
using Code.Gameplay.Tokens;
using Code.Levels.LevelGeneration.LeverEditor;
using UnityEngine;

namespace Code.Levels
{
	[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
	public class Level : ScriptableObject
	{
		[SerializeField] private int _actionsCount;
		[SerializeField] private List<Goal> _victoryConditions;
		[SerializeField] private ArrayLayout<TokenUnit> _tokens;

		public int ActionCount => _actionsCount;
		
		public TokenUnit[,] TokenTypesArray => _tokens.ToRectangularArray();

		private void OnValidate() => _actionsCount = _actionsCount > 0 ? _actionsCount : 1;
	}
}