using System;
using Code.UI.Windows.Service;
using TMPro;
using UnityEngine;

namespace Code.UI.Windows.Panels
{
	public class GameResultWindow : UnityWindow
	{
		[SerializeField] private TextMeshProUGUI _loseTextMesh;
		[SerializeField] private TextMeshProUGUI _victoryTextMesh;

		public void Construct(SessionResult sessionResult) => ChoiceTextMesh(sessionResult).enabled = true;
		
		private TextMeshProUGUI ChoiceTextMesh(SessionResult sessionResult)
			=> sessionResult switch
			{
				SessionResult.Lose    => _loseTextMesh,
				SessionResult.Victory => _victoryTextMesh,
				_                     => throw new ArgumentException(nameof(sessionResult)),
			};
	}
}