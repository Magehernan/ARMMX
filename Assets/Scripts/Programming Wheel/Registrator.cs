using UnityEngine;

public class Registrator : MonoBehaviour {
	[SerializeField]
	private AxisTypes axis;
	[SerializeField]
	private Vector3 rotationMask;
	[SerializeField]
	private Vector3 rotationOffset;

	private Vector3 initialRotation;
	private Transform myTransform;

	private Transform currentCollision;
	private bool returning = true;

	private void Awake() {
		myTransform = transform;
		initialRotation = myTransform.localEulerAngles;
		Rigidbody rigidbody1 = GetComponent<Rigidbody>();
		rigidbody1.centerOfMass = Vector3.zero;
		rigidbody1.inertiaTensorRotation = Quaternion.identity;
	}

	private void FixedUpdate() {
		if (returning) {
			myTransform.localEulerAngles = new Vector3(Mathf.LerpAngle(initialRotation.x, myTransform.localEulerAngles.x, .6f), 0f, 0f);
			return;
		}
		int currentAxis = (int)axis;
		float catetoOpuesto = myTransform.position[currentAxis] - currentCollision.position[currentAxis];
		float hipotenusa = (currentCollision.position - myTransform.position).magnitude;
		float angle = Mathf.Acos(catetoOpuesto / hipotenusa) * Mathf.Rad2Deg;
		myTransform.localEulerAngles = rotationOffset + rotationMask * angle;
	}

	private void OnTriggerEnter(Collider collision) {
		currentCollision = collision.transform;
		returning = false;
	}

	private void OnTriggerExit(Collider other) {
		returning = true;
		currentCollision = null;
	}

}
