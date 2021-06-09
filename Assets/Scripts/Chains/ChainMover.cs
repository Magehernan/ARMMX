using UnityEngine;

public class ChainMover : MonoBehaviour {
	public enum Direction {
		Up,
		Down
	}

	[SerializeField]
	private Vector3 moveMask;
	[SerializeField]
	private int axis;
	[SerializeField]
	private Direction direction;

	private Transform myTransform;
	private Vector3 lastPosition;
	private Collider current = null;
	private bool isActive;

	private void Awake() {
		myTransform = transform;
		lastPosition = myTransform.position;
		isActive = false;
	}

	private void Update() {
		Vector3 deltaPosition = myTransform.position - lastPosition;
		float axisMovement = deltaPosition[axis];
		isActive = direction == Direction.Up ? axisMovement > 0f : axisMovement < 0f;

		if (current != null) {
			if (isActive) {
				Vector3 deltaMask = Vector3.Scale(deltaPosition, moveMask);
				current.GetComponent<Chain>().Move(deltaMask);
			} else {
				current = null;
			}
		}

		lastPosition = myTransform.position;
	}
	private void OnTriggerStay(Collider other) {
		if (!isActive || current != null) {
			return;
		}
		current = other;
	}
}