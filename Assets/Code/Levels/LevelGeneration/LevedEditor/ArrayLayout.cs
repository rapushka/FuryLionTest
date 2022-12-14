using System;
using Code.Extensions;
using Code.Inner;

namespace Code.Levels.LevelGeneration.LevedEditor
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

			result.DoubleFor((_, i, j) => result[i, j] = Rows[i].Value[j]);

			return result;
		}
	}
}