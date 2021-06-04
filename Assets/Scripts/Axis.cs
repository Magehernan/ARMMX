using UnityEngine;

public class Axis : MonoBehaviour {
	[SerializeField]
	private Vector3 axisRotation;

	[SerializeField]
	private Gear[] gears;

	public void Rotate(float originRotation) {
		transform.Rotate(axisRotation * originRotation);
		
		foreach (Gear gear in gears) {
			gear.AxisRotate(originRotation);
		}
	}
}
