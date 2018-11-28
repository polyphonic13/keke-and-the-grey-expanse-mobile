namespace Polyworks {
	using UnityEngine;

    public abstract class GameEntity: MonoBehaviour, IInitializable {
        public bool isInitOnAwake = true;
        
        public virtual void Init() {
//			Debug.Log ("GameEntity[" + this.name + "]/Init");
		}
		public virtual void Init(IData data) {}
		public virtual void Init(IData[] data) {}
    }
}