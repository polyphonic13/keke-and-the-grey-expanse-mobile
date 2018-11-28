namespace Polyworks
{
	using System;

	[Serializable]
	public class Data: IData
	{
		public string created;
		public string updated;
		public string id { get; set; }

		public Data ()
		{
		}

		public virtual Data Clone ()
		{
			Data clone = new Data ();
			clone.id = this.id;
			clone.created = this.created;
			clone.updated = this.updated; 
			return clone;
		}

		public static T[] CloneChildren<T> (Data[] children) where T: Data
		{
			T[] clone = new T[children.Length];
			for (int i = 0, l = children.Length; i < l; i++) {
				clone [i] = children [i].Clone () as T;
			}

			return clone;
		}

		public static T[,] CloneMatrix<T> (Data[,] children) where T: Data
		{
			int size = children.GetLength (0);
			T[,] clone = new T[size, size];

			for (int i = 0; i < size; i++) {
				for (int j = 0; j < size; j++) {
					clone [i, j] = children [i, j].Clone () as T;
				}
			}

			return clone;
		}
	}
}

