namespace Code.Infrastructure.BaseSignals
{
	public abstract class ImmutableSignal<T>
	{
		protected ImmutableSignal(T value) => Value = value;

		public T Value { get; }
	}
	
	public abstract class ImmutableSignal<T1, T2>
	{
		protected ImmutableSignal(T1 value1, T2 value2)
		{
			Value1 = value1;
			Value2 = value2;
		}

		public T1 Value1 { get; }
		public T2 Value2 { get; }
	}
}