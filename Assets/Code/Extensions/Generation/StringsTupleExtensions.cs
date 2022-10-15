namespace Code.Extensions.GoogleSheetsParsing
{
	public static class StringsTupleExtensions
	{
		public static string ForMethod(this (string type, string name) @this) 
			=> $"{@this.type} {@this.name}, ";

		public static string ForInvoke(this (string type, string name) @this)
			=> $"(nameof({@this.name}), {@this.name}), ";
	}
}