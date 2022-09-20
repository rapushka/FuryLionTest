using System;
using Code.Common;

namespace Code.Levels
{
	[Serializable]
	public class ArrayLayout<T>
	{
		public Row<T>[] Rows;

		public ArrayLayout() => Rows = new Row<T>[Constants.GameFieldSize.Height];

		public T[,] ToRectangularArray()
		{
			var xLength = Rows.Length;
			var yLength = Rows[0].Value.Length;
			var result = new T[xLength, yLength];

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