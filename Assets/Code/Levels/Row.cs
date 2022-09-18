using System;
using Code.Common;
using Code.Gameplay;

namespace Code.Levels
{
	[Serializable]
	public class Row<T>
	{
		public T[] Value;

		public Row()
		{
			Value = new T[Constants.GameFieldSize.Width];
		}
	}
}