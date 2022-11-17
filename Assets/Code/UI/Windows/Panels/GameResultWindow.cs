using System;
using Code.UI.Windows.Service;
using UnityEngine;

namespace Code.UI.Windows.Panels
{
	public class GameResultWindow : UnityWindow
	{
		[SerializeField] private GameObject _loseView;
		[SerializeField] private GameObject _victoryView;

		public void Construct(SessionResult sessionResult) => ChoiceTextMesh(sessionResult).SetActive(true);

		private GameObject ChoiceTextMesh(SessionResult sessionResult)
			=> sessionResult switch
			{
				SessionResult.Lose    => _loseView,
				SessionResult.Victory => _victoryView,
				_                     => throw new ArgumentException(nameof(sessionResult)),
			};
	}
}