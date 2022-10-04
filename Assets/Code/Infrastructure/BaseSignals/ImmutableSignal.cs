namespace Code.Infrastructure.BaseSignals
{
	public abstract class ImmutableSignal<T>
	{
		protected ImmutableSignal(T value) => Value = value;

		public T Value { get; }
	}
}