using Code.Workflow.Extensions;
using UnityEngine;

namespace Code.Environment
{
	public class Field : MonoBehaviour
	{
		[SerializeField] private Token.Token[] _tokens;
		
		private LineDrawer _lineDrawer;
		private bool _chainStarted;

		public void Construct(LineDrawer lineDrawer)
		{
			_lineDrawer = lineDrawer;
		}

		public void StartChain(Vector2 position)
		{
			_chainStarted = true;
			_lineDrawer.AddTokenPosition(position);
		}

		public void AddTokenToChain(Vector2 position)
		{
			position.Do(_lineDrawer.AddTokenPosition, @if: _chainStarted);
		}

		public void EndChain()
		{
			_lineDrawer.ClearTokens();
			_chainStarted = false;
		}
	}
}