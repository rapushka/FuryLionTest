using Code.Gameplay.Tokens;
using Code.Levels.LevelGeneration.LeverEditor;
using UnityEngine;

namespace Code.Levels
{
	[CreateAssetMenu(fileName = "Level ", menuName = "ScriptableObjects/Level", order = 0)]
	public class Level : ScriptableObject
	{
		[SerializeField] private ArrayLayout<TokenType> _tokens;

		public TokenType[,] GetArray() => _tokens.ToRectangularArray();
	}
}