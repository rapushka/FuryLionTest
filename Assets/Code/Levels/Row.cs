using System;
using Code.Common;

namespace Code.Levels
{
	[Serializable]
	public class Row<T>
	{
		public T[] Value;

		public Row() => Value = new T[Constants.GameFieldSize.Width];
	}
}