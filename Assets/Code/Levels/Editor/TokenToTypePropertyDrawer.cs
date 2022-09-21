using System;
using Code.Common;
using Code.Gameplay;
using Code.Levels.LevelGeneration;
using UnityEditor;
using UnityEngine;

namespace Code.Levels.Editor
{
	[CustomPropertyDrawer(typeof(TokenToTypeCollection))]
	public class TokenToTypePropertyDrawer : PropertyDrawer
	{
		private const float ElementHeight = 25f;
		private const int ElementsInRow = 2;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			EditorGUI.PrefixLabel(position, label);

			var newPosition = position;
			newPosition.y += ElementHeight;
			var entries = property.FindPropertyRelative("_entries");
			var tokenTypesCount = Enum.GetValues(typeof(TokenType)).Length;

			entries.arraySize = tokenTypesCount;
			// for (var i = 0; i < tokenTypesCount; i++)
			// {
				// entries.InsertArrayElementAtIndex(0);
			// }
			
			for (var i = 0; i < entries.arraySize; i++)
			{
				var type = entries.GetArrayElementAtIndex(i).FindPropertyRelative("_tokenType");
				var prefab = entries.GetArrayElementAtIndex(i).FindPropertyRelative("_tokenPrefab");

				newPosition.height = ElementHeight;
				newPosition.width = position.width / ElementsInRow;

				type.enumValueIndex = i;
				DrawRow(type, prefab, newPosition);

				newPosition.x = position.x;
				newPosition.y += ElementHeight;
			}

			EditorGUI.EndProperty();
		}

		private void DrawRow(SerializedProperty type, SerializedProperty prefab, Rect newPosition)
		{
			newPosition.width *= 0.75f;
			EditorGUI.PropertyField(newPosition, type, GUIContent.none);
			newPosition.x += newPosition.width;
			newPosition.width *= 1.7f;
			EditorGUI.PropertyField(newPosition, prefab, GUIContent.none);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return ElementHeight * (Constants.GameFieldSize.Height + 1);
		}
	}
}