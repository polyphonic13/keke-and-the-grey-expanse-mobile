namespace Polyworks {
    using System.Runtime.Serialization.Formatters.Binary;
    using System.IO;
	using System;

	public class DataIOController {
		public delegate void OnData<T>(T data);

		public static void Save<T>(string url, T data) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (url);

			bf.Serialize (file, data);
			file.Close ();
		}

		public static void Load<T> (string url, OnData<T> callback) where T: new()
		{
			if (File.Exists (url)) {
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Open (url, FileMode.Open);

				T data = (T)bf.Deserialize (file);
				file.Close ();

				callback(data);
			} else {
				callback(new T());
			}
		}

		public static void Delete(string url) 
		{
			System.IO.File.Delete(url);
		}
	}
}