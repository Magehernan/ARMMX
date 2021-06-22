using UnityEngine;

public class Gear : MonoBehaviour {
	[SerializeField]
	private float radio;

	[SerializeField]
	private Vector3 axisRotation;

	[SerializeField]
	private Optional<Axis> axis;

	[SerializeField]
	private Gear[] gears;

	internal void AxisRotate(float originRotation) {
		transform.Rotate(originRotation * axisRotation);

		foreach (Gear gear in gears) {
			gear.Rotate(originRotation, radio);
		}
	}

	internal void Rotate(float originRotation, float originalRadio) {
		float rotation = originRotation * originalRadio / radio;
		transform.Rotate(rotation * axisRotation);

		if (axis) {
			axis.Value.Rotate(rotation);
		}

		foreach (Gear gear in gears) {
			gear.Rotate(rotation, radio);
		}
	}
}
