using Code.Gameplay.Tokens;
using Code.Levels.LevelGeneration.LeverEditor;
using UnityEngine;

namespace Code.Levels
{
	[CreateAssetMenu(fileName = "Level ", menuName = "ScriptableObjects/Level", order = 0)]
	public class Level : ScriptableObject
	{
		[SerializeField] private int _actionsCount;
		[SerializeField] private ArrayLayout<TokenType> _tokens;

		public int ActionCount => _actionsCount;
		
		public TokenType[,] TokenTypesArray => _tokens.ToRectangularArray();
	}
}