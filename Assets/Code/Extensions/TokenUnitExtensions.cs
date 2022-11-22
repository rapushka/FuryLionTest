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
		
		public static bool IsColor(this TokenUnit @this)
			=> @this switch
			{
				TokenUnit.Empty      => false,
				TokenUnit.Red        => true,
				TokenUnit.Green      => true,
				TokenUnit.Blue       => true,
				TokenUnit.Yellow     => true,
				TokenUnit.Pink       => true,
				TokenUnit.RockLevel1 => false,
				TokenUnit.RockLevel2 => false,
				TokenUnit.Ice        => false,
				TokenUnit.Border     => false,
				_                    => throw new ArgumentOutOfRangeException(nameof(@this), @this, null)
			};
	}
}