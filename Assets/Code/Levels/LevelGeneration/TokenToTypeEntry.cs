using System;
using Code.Gameplay.Tokens;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Levels.LevelGeneration
{
	[Serializable]
	public class TokenToTypeEntry
	{
		[FormerlySerializedAs("_type")] [SerializeField] private TokenUnit _unit;
		[SerializeField] private Token _prefab;

		public TokenUnit Unit => _unit;

		public Token Prefab => _prefab;
	}
}