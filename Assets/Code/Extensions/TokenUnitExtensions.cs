using System;
using Code.Gameplay.Tokens;
using UnityEngine;

namespace Code.Extensions
{
	public static class TokenUnitExtensions
	{
		public static Color GetColor(this TokenUnit @this)
			=> @this switch
			{
				TokenUnit.Empty      => Color.clear,
				TokenUnit.Red        => Color.red,
				TokenUnit.Green      => Color.green,
				TokenUnit.Blue       => Color.blue,
				TokenUnit.Yellow     => Color.yellow,
				TokenUnit.Pink       => Color.magenta,
				TokenUnit.RockLevel1 => Color.gray,
				TokenUnit.RockLevel2 => Color.HSVToRGB(0.3f, 0.3f, 0.3f),
				TokenUnit.Ice        => Color.cyan,
				TokenUnit.Border     => Color.black,
				_                    => throw new ArgumentOutOfRangeException(nameof(@this), @this, null)
			};
	}
}