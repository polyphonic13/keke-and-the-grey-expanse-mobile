using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatableData : ScriptableObject {

	public event System.Action OnValuesUpdated; 
	public bool isAutoUpdate; 

	protected virtual void OnValidate() {
		if (isAutoUpdate) {
//			NotifyUpdated ();
			UnityEditor.EditorApplication.update += NotifyUpdated;
		}
	}

	public void NotifyUpdated() {
		UnityEditor.EditorApplication.update -= NotifyUpdated;

		if(OnValuesUpdated != null) {
			OnValuesUpdated();
		}
	}

}
