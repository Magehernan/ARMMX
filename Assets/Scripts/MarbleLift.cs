using System.Collections.Generic;
using UnityEngine;

public class MarbleLift : MonoBehaviour {

	[SerializeField]
	private bool onEnter;

	[SerializeField]
	private bool onExit;

	//private List<GameObject> marbles = new List<GameObject>();


	//private void Update() {
	//	foreach (var item in marbles) {

	//	}
	//}

	private void OnTriggerEnter(Collider other) {
		if (!onEnter) {
			return;
		}

		if (other.tag.Equals("Marble")) {
			//marbles.Add(other.gameObject);
			other.attachedRigidbody.useGravity = false;
			other.attachedRigidbody.constraints = RigidbodyConstraints.FreezePositionX;
		}
	}

	private void OnTriggerExit(Collider other) {
		if (!onExit) {
			return;
		}

		if (other.tag.Equals("Marble")) {
			other.attachedRigidbody.useGravity = true;
			other.attachedRigidbody.constraints = RigidbodyConstraints.None;
		}
	}
}
