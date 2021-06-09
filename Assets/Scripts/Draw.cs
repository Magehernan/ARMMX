using UnityEngine;
public class Draw {
	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	public static void Ellipse(Vector3 position, float radiusX, float radiusY, int segments, UnityEngine.Color color, float duration = 0) {
		Ellipse(position, Vector3.forward, Vector3.up, radiusX, radiusY, segments, color, duration);
	}

	[System.Diagnostics.Conditional("UNITY_EDITOR")]
	public static void Ellipse(Vector3 position, Vector3 forward, Vector3 up, float radiusX, float radiusY, int segments, UnityEngine.Color color, float duration = 0) {
		float angle = 0f;
		Quaternion rot = Quaternion.LookRotation(forward, up);
		Vector3 lastPoint = Vector3.zero;
		Vector3 thisPoint = Vector3.zero;

		for (int i = 0; i < segments + 1; i++) {
			thisPoint.x = Mathf.Sin(Mathf.Deg2Rad * angle) * radiusX;
			thisPoint.y = Mathf.Cos(Mathf.Deg2Rad * angle) * radiusY;

			if (i > 0) {
				Debug.DrawLine(rot * lastPoint + position, rot * thisPoint + position, color, duration);
			}

			lastPoint = thisPoint;
			angle += 360f / segments;
		}
	}
}