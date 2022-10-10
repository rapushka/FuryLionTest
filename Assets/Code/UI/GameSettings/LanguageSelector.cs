using System;
using System.Linq;
using Code.DataStoring.Localizations;
using TMPro;
using UnityEngine;

namespace Code.UI.GameSettings
{
	public class LanguageSelector : MonoBehaviour
	{
		[SerializeField] private TMP_Dropdown _dropdown;

		private void OnEnable()
		{
			_dropdown.AddOptions(Enum.GetNames(typeof(LanguageLocale)).ToList());
		}
	}
}