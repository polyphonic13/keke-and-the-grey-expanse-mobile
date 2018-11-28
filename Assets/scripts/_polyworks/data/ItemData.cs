using System;
using UnityEngine;

namespace Polyworks
{
	[Serializable]
	public class ItemData : Data
	{
		public string displayName;
		public string prefabPath;
		public string thumbnail = "";

		public ItemData ()
		{
		}

		public override Data Clone ()
		{
			ItemData clone = base.Clone () as ItemData;
			clone.displayName = this.displayName;
			clone.prefabPath = this.prefabPath;
			clone.thumbnail = this.thumbnail;
			
			return clone as ItemData;
		}
	}

}

