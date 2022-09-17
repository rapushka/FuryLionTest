using Code.Environment;
using Code.Input;
using UnityEngine;

namespace Code.Infrastructure
{
	public class Bootstrapper : MonoBehaviour
	{
		[SerializeField] private LineRenderer _lineRenderer;
		[SerializeField] private OverlapMouse _overlapMouse;
		
		private void Awake()
		{
			var lineDrawer = new LineDrawer(_lineRenderer);

			_overlapMouse.TokenTouched += lineDrawer.OnTokenTouched;
		}
	}
}