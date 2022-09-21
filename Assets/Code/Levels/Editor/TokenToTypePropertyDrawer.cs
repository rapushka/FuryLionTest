using System;
using Code.Extensions;
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
		private const float CoefficientWidthType = 0.75f;
		private const float CoefficientWidthPrefab = 1.25f;

		private int _tokenTypesCount;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			EditorGUI.PrefixLabel(position, label);

			var currentPosition = position.AddY(ElementHeight);
			var entries = property.FindPropertyRelative("_entries");

			_tokenTypesCount = Enum.GetValues(typeof(TokenType)).Length;
			entries.arraySize = _tokenTypesCount;

			for (var i = 0; i < entries.arraySize; i++)
			{
				var entry = entries.GetArrayElementAtIndex(i);
				var type = entry.FindPropertyRelative("_type");
				var prefab = entry.FindPropertyRelative("_prefab");

				currentPosition = currentPosition.SetHeight(ElementHeight)
				                                 .SetWidth(position.width / ElementsInRow);

				type.enumValueIndex = i;
				DrawRow(type, prefab, currentPosition);

				currentPosition = currentPosition.SetX(position.x)
				                                 .AddY(ElementHeight);
			}

			EditorGUI.EndProperty();
		}

		private void DrawRow(SerializedProperty type, SerializedProperty prefab, Rect currentPosition)
		{
			var typeName = Enum.GetName(typeof(TokenType), type.enumValueIndex);
			var guiContent = new GUIContent(typeName);

			EditorGUI.PropertyField(currentPosition, type, guiContent);

			currentPosition = currentPosition.AddX(currentPosition.width * CoefficientWidthType)
			                                 .MultipleWidth(CoefficientWidthPrefab);

			EditorGUI.PropertyField(currentPosition, prefab, GUIContent.none);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return ElementHeight * (_tokenTypesCount + 1);
		}
	}
}