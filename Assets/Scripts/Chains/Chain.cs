using UnityEngine;

public class Chain : MonoBehaviour {
	[SerializeField]
	private ChainGears chainGears;
	[SerializeField]
	private Transform firstPivot;
	[SerializeField]
	private Transform secondPivot;
	[SerializeField]
	private Optional<Chain> nextChain;
	[SerializeField]
	private Vector3 rotationAxis;
	[SerializeField]
	private Vector3 rotation;

	[SerializeField]
	private bool isActive = false;



	private void Update() {
		if (!isActive) {
			return;
		}

		Apply(firstPivot.position, this);
	}

	public void Move(Vector3 delta) {
		Apply(firstPivot.position + delta, this);
	}

	private void Apply(Vector3 current, Chain chainStart) {
		bool found = false;
		Draw.Ellipse(current, .0001f, .0001f, 30, Color.red);
		Draw.Ellipse(secondPivot.position, .0001f, .0001f, 30, Color.red);

		foreach (ChainGear gear in chainGears.gears) {
			Vector3 directionFirst = current - gear.transform.position;
			directionFirst.z = 0;
			if (directionFirst.sqrMagnitude < gear.SqrtRadius) {
				current = gear.transform.position + directionFirst.normalized * gear.radius;
			}

			Vector3 directionSecond = secondPivot.position - gear.transform.position;
			directionSecond.z = 0;
			if (directionSecond.sqrMagnitude < gear.SqrtRadius) {
				Vector3 direction = Vector3.LerpUnclamped(current, secondPivot.position, .5f) - gear.transform.position;
				float gearAngle = Vector3.SignedAngle(direction, Vector3.up, rotationAxis) + 90;
				firstPivot.localEulerAngles = rotation * gearAngle;
				break;
			}
		}

		firstPivot.position = current;

		if (nextChain) {
			if (!found) {
				float angle = Vector3.SignedAngle(nextChain.Value.firstPivot.position - firstPivot.position, Vector3.up, rotationAxis);
				firstPivot.localEulerAngles = rotation * angle;
			}

			if (nextChain.Value != chainStart) {
				nextChain.Value.Apply(secondPivot.position, chainStart);
			}
		}
	}
}
