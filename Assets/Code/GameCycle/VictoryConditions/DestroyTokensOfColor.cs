using System;
using Code.Gameplay.Tokens;
using UnityEngine;

namespace Code.GameCycle.VictoryConditions
{
	[Serializable]
	public class DestroyTokensOfColor : VictoryCondition
	{
		[SerializeField] private TokenColor _color;

		public TokenColor Color => _color;
	}
}