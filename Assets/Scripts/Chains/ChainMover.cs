using System;
using UnityEngine;
using UnityEngine.Events;

public class ChainMover : MonoBehaviour {
	public enum Direction {
		Up,
		Down
	}

	[SerializeField]
	private Vector3 moveMask;
	[SerializeField]
	private AxisTypes axis;
	[SerializeField]
	private Direction direction;
	[SerializeField]
	private UnityEvent<float> onMove;

	private Transform myTransform;
	private Vector3 lastPosition;
	private Collider current = null;
	private bool isActive;


	private void Awake() {
		myTransform = transform;
		lastPosition = myTransform.position;
		isActive = false;
	}

	private void FixedUpdate() {
		Vector3 deltaPosition = myTransform.position - lastPosition;
		float axisMovement = deltaPosition[(int)axis];
		isActive = direction == Direction.Up ? axisMovement > 0f : axisMovement < 0f;

		if (current != null) {
			if (isActive) {
				onMove?.Invoke(Math.Abs(axisMovement));
				Vector3 deltaMask = Vector3.Scale(deltaPosition, moveMask);
				current.GetComponent<Chain>().Move(deltaMask);
			} else {
				current = null;
			}
		}

		lastPosition = myTransform.position;
	}
	private void OnTriggerEnter(Collider other) {
		if (!isActive || current != null) {
			return;
		}
		current = other;
	}
}