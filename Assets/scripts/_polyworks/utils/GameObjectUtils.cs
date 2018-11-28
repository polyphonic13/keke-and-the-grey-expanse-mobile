namespace Polyworks {
	using UnityEngine;
	using System.Collections;

	public class GameObjectUtils : MonoBehaviour
	{
		public static readonly Vector3 SCALE_ONE = new Vector3 (1, 1, 1);
		public static void DeactivateFromTransforms(Transform[] transforms) {
			for (int i = 0; i < transforms.Length; i++) {
				transforms [i].gameObject.SetActive (false);
			}
		}

		public static GameObject InstantiateObject(string prefabPath, Vector3 location, Vector3 rotation, string name = "") 
		{

			GameObject go = (GameObject) Instantiate (Resources.Load (prefabPath, typeof(GameObject)), location, Quaternion.Euler(rotation));

			if (name != "") {
				go.name = name;
			}
			return go;
		}
	}
}
