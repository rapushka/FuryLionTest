using System;
using Code.Common;
using Code.Gameplay;

namespace Code.Levels
{
	[Serializable]
	public class Row
	{
		public TokenType[] Value;

		public Row()
		{
			Value = new TokenType[Constants.GameFieldSize.Width];
		}
	}
}