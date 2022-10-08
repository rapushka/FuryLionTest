using System;
using Code.Inner;

namespace Code.Levels.LevelGeneration.LevedEditor
{
	[Serializable]
	public class Row<T>
	{
		public T[] Value;

		public Row() => Value = new T[Constants.GameFieldSize.Width];
	}
}