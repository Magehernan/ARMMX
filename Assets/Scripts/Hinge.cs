using UnityEngine;

public class Hinge : MonoBehaviour {
	[SerializeField]
	private Transform originTransform;
	[SerializeField]
	private Optional<Transform> centerRotation;
	[SerializeField]
	private int axis;
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

	private void Update() {

		Vector3 position = originTransform.position;
		myTransform.position = position;
		if (centerRotation) {
			myTransform.eulerAngles = eulerRotation - rotationMask * (
				Mathf.Acos((position[axis] - centerRotation.Value.position[axis]) / (extremeTransform.position - position).magnitude)
				* Mathf.Rad2Deg
			);
		}
	}
}
