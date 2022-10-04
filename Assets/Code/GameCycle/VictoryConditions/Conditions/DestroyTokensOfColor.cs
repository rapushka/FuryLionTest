using System;
using Code.GameCycle.VictoryConditions.TokensTypes;
using UnityEngine;

namespace Code.GameCycle.VictoryConditions.Conditions
{
	[Serializable]
	public class DestroyTokensOfColor : VictoryCondition
	{
		[SerializeField] private TokenColor _color;

		public TokenColor Color => _color;
	}
}