using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Code.DataStoring
{
	// ReSharper disable once ClassNeverInstantiated.Global класс создаётся через Zenject
	public class BinaryStorage : IStorage
	{
		private readonly BinaryFormatter _formatter;

		public BinaryStorage() => _formatter = new BinaryFormatter();

		public void Save<T>(T data)
		{
			var filePath = GetFilePathForType<T>();
			using var file = File.Create(filePath);
			_formatter.Serialize(file, data);
		}

		public T Load<T>(T defaultData)
		{
			var filePath = GetFilePathForType<T>();
			if (File.Exists(filePath) == false)
			{
				Save(defaultData);
				return defaultData;
			}

			using var file = File.Open(filePath, FileMode.Open);
			var loadedData = _formatter.Deserialize(file);

			return (T)loadedData;
		}

		private static string GetFilePathForType<T>() 
			=> $"{Application.persistentDataPath}/{typeof(T).Name}.save";
	}
}