namespace keke 
{
    using System;
    using UnityEngine;

    public class SceneAgent: CoreObject {
        
        public SceneType type;
        
        public void Init() {
			// GameEntity[] gameEntities;
			// gameEntities = GetComponentsInChildren<GameEntity>();
//			Debug.Log ("SceneAgent/Init, gameEntites = " + gameEntities.Length);

			// if (gameEntities != null) {
				// foreach (GameEntity entity in gameEntities) {
//					Debug.Log (" entity[" + entity.gameObject.name + "].isInitOnAwake = " + entity.isInitOnAwake);
					// if ((entity.transform != this.transform) && entity.isInitOnAwake) {
						// entity.Init ();
					// }
				// }        
			// }
        }

		private void Awake()
		{
			Debug.Log("SceneAgent[ " + this.name + " ]/Awake");
		}
   }
}