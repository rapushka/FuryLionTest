using System;
using Code.Gameplay;
using UnityEngine;

namespace Code.Extensions
{
	public static class TokenTypeExtensions
	{
		public static Color GetColor(this TokenType @this)
			=> @this switch
			{
				TokenType.Empty       => Color.clear,
				TokenType.Red         => Color.red,
				TokenType.Green       => Color.green,
				TokenType.Blue        => Color.blue,
				TokenType.Yellow      => Color.yellow,
				TokenType.Pink        => Color.magenta,
				TokenType.RockLevel1        => Color.gray,
				TokenType.RockLevel2 => Color.HSVToRGB(0.3f, 0.3f, 0.3f),
				TokenType.Ice         => Color.cyan,
				TokenType.Border      => Color.black,
				TokenType.RandomColor      => Color.white,
				_                     => throw new ArgumentOutOfRangeException(nameof(@this), @this, null)
			};
	}
}