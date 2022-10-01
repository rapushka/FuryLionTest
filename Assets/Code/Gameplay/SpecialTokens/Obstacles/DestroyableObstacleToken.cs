using Code.Environment;
using Zenject;

namespace Code.Gameplay.SpecialTokens.Obstacles
{
	public class DestroyableObstacleToken : BaseObstacleToken
	{
		private Field _field;

		[Inject]
		public void Construct(Field field)
		{
			_field = field;
		}
		
		protected override void ApplyMatchInNeighbour()
		{
			_field.DestroyTokenAt(Token.transform.position);
		}
	}
}