using Code.GameCycle.VictoryConditions.TokensTypes;
using UnityEngine;

namespace Code.GameCycle.VictoryConditions.Conditions
{
	[CreateAssetMenu(fileName = FileName, menuName = MenuName)]
	public class DestroyTokensOfColor : VictoryCondition
	{
		[SerializeField] private TokenColor _color;
		
		private const string FileName = "Destroy X Tokens Of Color X";
		private const string MenuName = "ScriptableObjects/VictoryCondition/DestroyTokensOfColor";

		public TokenColor Color => _color;
	}
}