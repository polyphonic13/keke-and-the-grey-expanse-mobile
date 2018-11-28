namespace Polyworks {

	using UnityEngine;
	using System.Collections;
	using System.Collections.Generic;

	public class RaycastAgent : MonoBehaviour {

		public float interactDistance = 4f;
		public string targetTag = "";

		private ProximityAgent _focusedItem;
		private string _itemJustHit = "";

		private bool _isActive = true;

		private void Update () {
			if (_isActive) {
				_checkRayCast ();
			}
		}

		private void _setFocus(ProximityAgent pa, Transform target) {
			if(pa != null) {
				if (pa.Check (this.transform)) {
					pa.SetFocus (true);
					_itemJustHit = target.name;
					_focusedItem = pa;
					Debug.Log ("    hit was in proximity");
					gameObject.SendMessage ("BeginCollidableCollision", target, SendMessageOptions.DontRequireReceiver);
				}
			}
		}

		private void _clearFocus() {
			if(_focusedItem != null) {
				_focusedItem.SetFocus (false);
				_focusedItem = null;
				gameObject.SendMessage ("EndCollidableCollision", null, SendMessageOptions.DontRequireReceiver);
			}
			_itemJustHit = "";
		}

		private void _checkRayCast() {
			RaycastHit hit;
			Debug.DrawRay (this.transform.position, this.transform.forward, Color.red);
			if (Physics.Raycast (this.transform.position, this.transform.forward, out hit, interactDistance)) {

				if (hit.transform != this.transform && hit.transform.tag == targetTag) {

					if (hit.transform.name != _itemJustHit) {
//						Debug.Log ("   hit something: " + hit.transform.name);
						_setFocus(hit.transform.gameObject.GetComponent<ProximityAgent> (), hit.transform);
					}
				} else {
					_clearFocus();
				}
			} else {
				_clearFocus();
			}
		}

		bool IsLookingAtObject(Transform obj, Transform targetObj) {
			Vector3 direction = targetObj.position - obj.position;

			float ang = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

			//current angle in degrees
			float anglecurrent = obj.eulerAngles.z;
			float checkAngle = 0;

			//Checking to see what quadrant current angle is at
			if (anglecurrent > 0 && anglecurrent <= 90) {
				checkAngle = ang - 90;
			}
			if (anglecurrent > 90 && anglecurrent <= 360) {
				checkAngle = ang + 270;
			}

			//If current angle is equal to the angle that I need to be at to look at the object, return true
			//It is possible to do "if (checkAngle == anglecurrent)" but some times you don't get an exact match so do something like below to have some error range.

			if (anglecurrent <= checkAngle + 0.5f && anglecurrent >= checkAngle - 0.5f) {
				return true;  
			} else {
				return false;
			}
		}
	}
}
