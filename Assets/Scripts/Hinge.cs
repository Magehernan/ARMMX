using UnityEngine;

public class Hinge : MonoBehaviour {
	[SerializeField]
	private Transform originTransform;
	[SerializeField]
	private Optional<Transform> centerRotation;
	[SerializeField]
	private AxisTypes axis;
	[SerializeField]
	private Vector3 rotationMask;
	[SerializeField]
	private Transform extremeTransform;
	[SerializeField]
	private Optional<Vector3> eulerRotation;

	private Transform myTransform;

	private void Awake() {
		myTransform = transform;
	}

	private void FixedUpdate() {
		Vector3 position = originTransform.position;
		myTransform.position = position;
		if (centerRotation) {
			int currentAxis = (int)axis;
			myTransform.eulerAngles = eulerRotation - rotationMask * (
				Mathf.Acos((position[currentAxis] - centerRotation.Value.position[currentAxis]) / (extremeTransform.position - position).magnitude)
				* Mathf.Rad2Deg
			);
		}
	}
}
