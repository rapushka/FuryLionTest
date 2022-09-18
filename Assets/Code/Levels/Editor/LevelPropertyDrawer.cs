using System.Collections.Generic;
using Code.Gameplay;
using UnityEditor;
using UnityEngine;
using static Code.Common.Constants;

namespace Code.Levels.Editor
{
	[CustomPropertyDrawer(typeof(ArrayLayout))]
	public class LevelPropertyDrawer : PropertyDrawer
	{
		private const float ElementHeight = 36f;

		private TokenTypeToColor _tokenTypeToColor;


		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			EditorGUI.PrefixLabel(position, label);
			
			var newPosition = position;
			newPosition.y += ElementHeight;
			var rows = property.FindPropertyRelative("Rows");
			
			for (var y = 0; y < GameFieldSize.Height; y++)
			{
				var row = rows.GetArrayElementAtIndex(y).FindPropertyRelative("Value");
				newPosition.height = ElementHeight;

				newPosition.width = position.width / GameFieldSize.Width;
				DrawRow(row, newPosition, y);

				newPosition.x = position.x;
				newPosition.y += ElementHeight;
			}
			EditorGUI.EndProperty();
		}

		private void DrawRow(SerializedProperty row, Rect newPosition, int y)
		{
			for (var x = 0; x < GameFieldSize.Width; x++)
			{
				var element = row.GetArrayElementAtIndex(x);

				EditorGUI.PropertyField(newPosition, element, GUIContent.none);
				newPosition.x += newPosition.width;
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return ElementHeight * (GameFieldSize.Height + 1);
		}
	}
}