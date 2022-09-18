using System;
using Code.Common;
using Code.Gameplay;

namespace Code.Levels
{
	[Serializable]
	public class ArrayLayout
	{
		public Row[] Rows;

		public ArrayLayout()
		{
			Rows = new Row[Constants.GameFieldSize.Height];
		}

		public TokenType[,] ToRectangularArray()
		{
			var xLength = Rows.Length;
			var yLength = Rows[0].Value.Length;
			var result = new TokenType[xLength, yLength];

			for (var i = 0; i < xLength; i++)
			{
				for (var j = 0; j < yLength; j++)
				{
					result[i, j] = Rows[i].Value[j];
				}
			}

			return result;
		}
	}
}