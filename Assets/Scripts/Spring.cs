using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {
	[SerializeField]
	private Transform originTransform;

	private Transform myTransform;

	private float initialSize;

	private void Awake() {
		myTransform = transform;
		initialSize = (originTransform.position - myTransform.position).magnitude;
	}

	private void FixedUpdate() {
		myTransform.localScale =  new Vector3(1f,1f, (originTransform.position - myTransform.position).magnitude / initialSize);
	}
}
