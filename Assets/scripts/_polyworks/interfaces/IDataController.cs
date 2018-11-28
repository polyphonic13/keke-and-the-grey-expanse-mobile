namespace Polyworks {
	using System;

    public interface IDataController {
        void Save<T>(string url, T data);
        void Load<T>(string url, Action<T> callback);
    }
}