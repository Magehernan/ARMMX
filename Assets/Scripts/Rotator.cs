using System;
using UnityEngine;

public class Rotator : MonoBehaviour {
	[SerializeField]
	private float rpm;

	[SerializeField]
	private Optional<Gear> gear;

	[SerializeField]
	private Optional<Axis> axis;

	private void Update() {
		if (gear) {
			gear.Value.Rotate(rpm / 60f * Time.deltaTime, 1);
		}

		if (axis) {
			axis.Value.Rotate(rpm / 60f * Time.deltaTime);
		}
	}

	internal void SetRPM(float rpm) {
		this.rpm = rpm;
	}
}
