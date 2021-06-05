using UnityEngine;

public class Hinge : MonoBehaviour {
	[SerializeField]
	private Transform originTransform;
	[SerializeField]
	private Optional<Vector3> eulerRotation;
	[SerializeField]
	private Vector3 minAngle;
	[SerializeField]
	private Vector3 maxAngle;

	private Transform myTransform;

	private void Awake() {
		myTransform = transform;
	}

	private void Update() {
		if (eulerRotation) {
			myTransform.eulerAngles = eulerRotation + Vector3.LerpUnclamped(maxAngle, minAngle, Mathf.Sin(originTransform.eulerAngles.z * Mathf.Deg2Rad));
		}

		myTransform.position = originTransform.position;
	}
}
