using Code.UI.Windows.Service;
using UnityEngine;

namespace Code.UI.Buttons
{
	public class ButtonClose : UnityActionToMethodAdapter
	{
		[SerializeField] private WindowsChain _chain;

		protected override void OnClick() => _chain.Close();
	}
}