using System.Collections.Generic;
using UnityEngine;

public class PinLane : MonoBehaviour {
	private const int PIN_AMOUNT = 64;
	private const float LANE_ANGLE = 90f;
	private const float PIN_ANGLE = LANE_ANGLE / PIN_AMOUNT;

	[SerializeField]
	private int frequency = 1;
	[SerializeField]
	private List<Transform> pins;

	private void OnValidate() {
		pins.Clear();
		for (int i = 0; i < transform.childCount; i++) {
			Transform current = transform.GetChild(i);
			pins.Add(current);
			current.localEulerAngles = new Vector3(-i * PIN_ANGLE, 0, 0);
			current.gameObject.SetActive(i % frequency == 0);
		}
	}
}
