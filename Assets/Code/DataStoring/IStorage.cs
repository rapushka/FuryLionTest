namespace Code.DataStoring
{
	public interface IStorage
	{
		void Save<T>(T data);
		
		T Load<T>(T defaultData);
	}
}