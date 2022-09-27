namespace Code.Infrastructure.BaseSignals
{
	public class ImmutableSignal<T>
	{
		public ImmutableSignal(T value)
		{
			Value = value;
		}

		public T Value { get; }
	}
}