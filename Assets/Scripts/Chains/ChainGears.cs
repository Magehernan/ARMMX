using System;
using System.Collections.Generic;
using UnityEngine;

public class ChainGears : MonoBehaviour {
	public List<ChainGear> gears;

	private void Update() {
		foreach (ChainGear gear in gears) {
			Draw.Ellipse(gear.transform.position, gear.radius, gear.radius, 30, Color.red);
		}
	}
}

[Serializable]
public class ChainGear : ISerializationCallbackReceiver {
	public Transform transform;
	public float radius;
	public float SqrtRadius { get; private set; }

	public void OnAfterDeserialize() {
		SqrtRadius = radius * radius;
	}

	public void OnBeforeSerialize() {
	}
}
