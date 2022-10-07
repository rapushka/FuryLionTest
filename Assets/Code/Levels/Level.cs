using System;
using System.Collections.Generic;
using System.Linq;
using Code.GameLoop.Goals.Conditions;
using Code.Gameplay.Tokens;
using Code.Levels.LevelGeneration.LeverEditor;
using UnityEngine;

namespace Code.Levels
{
	[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
	public class Level : ScriptableObject
	{
		[SerializeField] private int _actionsCount;
		[SerializeField] private List<Goal> _goals;
		[SerializeField] private ArrayLayout<TokenUnit> _tokens;

		public int ActionCount => _actionsCount;
		
		public TokenUnit[,] TokenTypesArray => _tokens.ToRectangularArray();

		public List<Goal> Goals => _goals;

		private void OnValidate()
		{
			_actionsCount = _actionsCount > 0 ? _actionsCount : 1;
			
			if (_goals.Count == 0 || _goals.Any((g) => g is null))
			{
				throw new ArgumentException("Level should have at least 1 Goal");
			}
		}
	}
}