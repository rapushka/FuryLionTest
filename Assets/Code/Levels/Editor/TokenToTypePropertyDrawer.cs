using System;
using Code.Extensions;
using Code.Gameplay.Tokens;
using Code.Levels.LevelGeneration;
using UnityEditor;
using UnityEngine;

namespace Code.Levels.Editor
{
	[CustomPropertyDrawer(typeof(SerializableTokensCollection))]
	public class TokenToTypePropertyDrawer : PropertyDrawer
	{
		private const float ElementHeight = 20f;
		private const int ElementsInRow = 2;
		private const float CoefficientWidthType = 0.75f;
		private const float CoefficientWidthPrefab = 1.25f;
		private const float Offset = 10f;
		private const float Spacing = 5f;

		private int _tokenTypesCount;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position = position.AddY(Offset);
			EditorGUI.PrefixLabel(position, label);

			var entries = property.FindPropertyRelative("_entries");
			
			_tokenTypesCount = Enum.GetValues(typeof(TokenUnit)).Length;
			entries.arraySize = _tokenTypesCount;

			var currentPosition = position.AddY(ElementHeight).SetHeight(ElementHeight);
			for (var i = 0; i < entries.arraySize; i++)
			{
				var entry = entries.GetArrayElementAtIndex(i);
				
				var type = entry.FindPropertyRelative("_unit");
				var prefab = entry.FindPropertyRelative("_prefab");

				currentPosition = currentPosition.SetWidth(position.width / ElementsInRow);
				type.enumValueIndex = i;

				DrawRow(type, prefab, currentPosition);
				
				currentPosition = ResetPositionToNextLine(position, currentPosition);
			}

			EditorGUI.EndProperty();
		}

		private void DrawRow(SerializedProperty type, SerializedProperty prefab, Rect currentPosition)
		{
			var typeName = Enum.GetName(typeof(TokenUnit), type.enumValueIndex);
			var guiContent = new GUIContent(typeName);
		
			EditorGUI.PropertyField(currentPosition, type, guiContent);

			currentPosition = currentPosition.AddX(currentPosition.width * CoefficientWidthType)
			                                 .MultipleWidth(CoefficientWidthPrefab);

			EditorGUI.PropertyField(currentPosition, prefab, GUIContent.none);
		}

		private static Rect ResetPositionToNextLine(Rect position, Rect currentPosition)
			=> currentPosition.SetX(position.x)
			                  .AddY(ElementHeight + Spacing);

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) 
			=> (ElementHeight + Spacing) * (_tokenTypesCount + 1);
	}
}