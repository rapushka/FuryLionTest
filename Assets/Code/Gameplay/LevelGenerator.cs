using System;
using Code.Common;
using Code.Levels;
using UnityEngine;

namespace Code.Gameplay
{
	public class LevelGenerator : MonoBehaviour
	{
		[SerializeField] private Level _debugLevel;

		public Token[,] Generate()
		{
			var tokens = new Token[Constants.GameFieldSize.Height, Constants.GameFieldSize.Width];
			var tokenTypes = _debugLevel.GetArray();

			throw new NotImplementedException();
		}
	}
}