using Code.Gameplay.Tokens;
using UnityEngine;

namespace Code.DataStoring.Localizations.LocalsVariables
{
	public class Values : MonoBehaviour
	{
		public int Count;
		public TokenUnit Color;

		public Values Set(int count, TokenUnit color)
		{
			Count = count;
			Color = color;
			return this;
		}
	}
}