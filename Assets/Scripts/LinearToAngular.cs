using UnityEngine;

public class LinearToAngular : MonoBehaviour {
	[SerializeField]
	private float radius;
	[SerializeField]
	private Vector3 rotateMask;

	private Transform myTransform;

	private float perimeter;
	private void Awake() {
		perimeter = 2f * Mathf.PI * radius;
		myTransform = transform;
	}

	public void RotateFromLinear(float movement) {
		myTransform.Rotate(rotateMask * movement / perimeter * 360f);
	}
}