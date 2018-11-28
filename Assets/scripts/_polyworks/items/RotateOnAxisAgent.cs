namespace Polyworks {
	using UnityEngine;

	public class RotateOnAxisAgent : GameEntity {

		public float speed = 0;
		public Vector3 axis = Vector3.up;
		public Space space = Space.World;

		private bool _isActive;

		public override void Init() {
//			Debug.Log ("RotateOnAxisAgent[" + this.name + "]/Init");
			base.Init ();
			if (isInitOnAwake) {
				Begin ();
			}
		}

		public void Begin() {
			if(!_isActive) {
				_isActive = true;
			}
		}

		public void End() {
			if(_isActive) {
				_isActive = false;
			}
		}
		
		private void FixedUpdate () {
			if(_isActive && speed > 0) {
				transform.Rotate(axis * speed * Time.deltaTime, space);
			}
		}
	}
}
