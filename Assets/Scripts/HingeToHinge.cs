using System;
using UnityEngine;

public class HingeToHinge : MonoBehaviour {
	[SerializeField]
	private Transform hinge1OriginTransform;
	[SerializeField]
	private Transform hinge1ExtremeTransform;
	[SerializeField]
	private Transform hinge2OriginTransform;
	[SerializeField]
	private Transform hinge2ExtremeTransform;
	[SerializeField]
	private PlaneTypes plane;

	private Transform myTransform;

	private float radius1;
	private float radius2;

	private void Awake() {
		myTransform = transform;
		radius1 = (hinge1ExtremeTransform.position - hinge1OriginTransform.position).magnitude;
		radius2 = (hinge2ExtremeTransform.position - hinge2OriginTransform.position).magnitude;
	}

	private void FixedUpdate() {
		Vector2 origin1 = PositionToPlane(plane, hinge1OriginTransform.position);
		Vector2 origin2 = PositionToPlane(plane, hinge2OriginTransform.position);
		int iAmount = FindCircleCircleIntersections(origin1, radius1, origin2, radius2, out Vector2 i1, out Vector2 i2);
		if (iAmount == 2) {
			Vector3 position = myTransform.position;
			switch (plane) {
				case PlaneTypes.XY:
					position.x = i1.x;
					position.y = i1.y;
					break;
				case PlaneTypes.XZ:
					position.x = i1.x;
					position.z = i1.y;
					break;
				case PlaneTypes.ZY:
					position.z = i1.x;
					position.y = i1.y;
					break;
				default:
					throw new InvalidOperationException($"Plane unimplemented {plane}");
			}
			position.z = i1.x;
			position.y = i1.y;
			myTransform.position = position;
		}
	}

	private int FindCircleCircleIntersections(Vector2 c0, float r0, Vector2 c1, float r1, out Vector2 intersection1, out Vector2 intersection2) {
		// Find the distance between the centers.
		double dx = c0.x - c1.x;
		double dy = c0.y - c1.y;
		double dist = Math.Sqrt(dx * dx + dy * dy);

		if (Math.Abs(dist - (r0 + r1)) < 0.00001) {
			intersection1 = Vector2.Lerp(c0, c1, r0 / (r0 + r1));
			intersection2 = intersection1;
			return 1;
		}

		// See how many solutions there are.
		if (dist > r0 + r1) {
			// No solutions, the circles are too far apart.
			intersection1 = new Vector2(float.NaN, float.NaN);
			intersection2 = new Vector2(float.NaN, float.NaN);
			return 0;
		} else if (dist < Math.Abs(r0 - r1)) {
			// No solutions, one circle contains the other.
			intersection1 = new Vector2(float.NaN, float.NaN);
			intersection2 = new Vector2(float.NaN, float.NaN);
			return 0;
		} else if ((dist == 0) && (r0 == r1)) {
			// No solutions, the circles coincide.
			intersection1 = new Vector2(float.NaN, float.NaN);
			intersection2 = new Vector2(float.NaN, float.NaN);
			return 0;
		} else {
			// Find a and h.
			double a = (r0 * r0 - r1 * r1 + dist * dist) / (2 * dist);
			double h = Math.Sqrt(r0 * r0 - a * a);

			// Find P2.
			double cx2 = c0.x + a * (c1.x - c0.x) / dist;
			double cy2 = c0.y + a * (c1.y - c0.y) / dist;

			// Get the points P3.
			intersection1 = new Vector2(
				(float)(cx2 + h * (c1.y - c0.y) / dist),
				(float)(cy2 - h * (c1.x - c0.x) / dist));
			intersection2 = new Vector2(
				(float)(cx2 - h * (c1.y - c0.y) / dist),
				(float)(cy2 + h * (c1.x - c0.x) / dist));

			return 2;
		}
	}


	private Vector2 PositionToPlane(PlaneTypes plane, Vector3 position) {
		switch (plane) {
			case PlaneTypes.XY:
				return new Vector2(position.x, position.y);
			case PlaneTypes.XZ:
				return new Vector2(position.x, position.z);
			case PlaneTypes.ZY:
				return new Vector2(position.z, position.y);
			default:
				throw new InvalidOperationException($"Plane unimplemented {plane}");
		}
	}
}