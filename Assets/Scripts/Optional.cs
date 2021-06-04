using System;
using UnityEngine;

[Serializable]
/// Requires Unity 2020.1+
public struct Optional<T> {
	[SerializeField] private bool enabled;
	[SerializeField] private T value;

	public T Value => value;

	public Optional(T initialValue) {
		enabled = true;
		value = initialValue;
	}

	public static implicit operator bool(Optional<T> optional) {
		return optional.enabled;
	}

	public static implicit operator T(Optional<T> optional) {
		return optional.value;
	}
}
