namespace Polyworks {
	using UnityEngine; 
	
	public class RotateAroundObjectAgent : GameEntity {
		
		public Transform center;
		
		public Vector3 axis = Vector3.up;
		public Vector3 desiredPosition;
		
		public float radius = 2.0f;
		public float radiusSpeed = 0;
		public float rotationSpeed = 0;
		
		private bool _isActive = false; 

		public override void Init() {
//			Debug.Log ("RotateAroundObject[" + this.name + "]/Init");
			base.Init ();

			if (isInitOnAwake) {
				Begin ();
			}
		}

		public void Begin() {
			if(_isActive != true) {
				transform.position = (transform.position - center.position).normalized * radius + center.position;
				_isActive = true;
			}
		}
		
		public void End() {
			if(_isActive) {
				_isActive = false;
			}
		}
		
		private void FixedUpdate() {
			if(_isActive == true) {
				transform.RotateAround(center.position, axis, rotationSpeed * Time.deltaTime);
				
				if(desiredPosition != null) {
					desiredPosition = (transform.position - center.position).normalized * radius + center.position;
					transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
				}
			}
		}
	}
}