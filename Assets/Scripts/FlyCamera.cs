using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class FlyCamera : MonoBehaviour {
	public float acceleration = 50; // how fast you accelerate
	public float accSprintMultiplier = 4; // how much faster you go when "sprinting"
	public float lookSensitivity = 1; // mouse look sensitivity
	public float dampingCoefficient = 5; // how quickly you break to a halt after you stop your input
	public bool focusOnEnable = true; // whether or not to focus and lock cursor immediately on enable

	Vector3 velocity; // current velocity

	bool Focused {
		get => Cursor.lockState == CursorLockMode.Locked;
		set {
			Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
			Cursor.visible = value == false;
		}
	}

	void OnEnable() {
		if (focusOnEnable)
			Focused = true;
	}

	void OnDisable() => Focused = false;

	void Update() {
		// Input
		if (Focused) {
			UpdateInput();
		} else if (Mouse.current.leftButton.wasPressedThisFrame && !EventSystem.current.IsPointerOverGameObject()) {
			Focused = true;
		}

		// Physics
		velocity = Vector3.Lerp(velocity, Vector3.zero, dampingCoefficient * Time.deltaTime);
		transform.position += velocity * Time.deltaTime;
	}

	void UpdateInput() {
		// Position
		velocity += GetAccelerationVector() * Time.deltaTime;

		// Rotation
		Vector2 mouseDelta = lookSensitivity * Vector2.Scale(Mouse.current.delta.ReadValue(), new Vector2(.8f, -.5f));
		transform.Rotate(Vector3.up, mouseDelta.x, Space.World);
		transform.Rotate(Vector3.right, mouseDelta.y, Space.Self);

		// Leave cursor lock
		if (Keyboard.current[Key.Escape].wasPressedThisFrame)
			Focused = false;
	}

	Vector3 GetAccelerationVector() {
		Vector3 moveInput = default;

		void AddMovement(Key key, Vector3 dir) {
			if (Keyboard.current[key].isPressed)
				moveInput += dir;
		}

		AddMovement(Key.W, Vector3.forward);
		AddMovement(Key.S, Vector3.back);
		AddMovement(Key.D, Vector3.right);
		AddMovement(Key.A, Vector3.left);
		AddMovement(Key.Space, Vector3.up);
		AddMovement(Key.LeftCtrl, Vector3.down);
		Vector3 direction = transform.TransformVector(moveInput.normalized);

		if (Keyboard.current[Key.LeftShift].isPressed) {
			return direction * (acceleration * accSprintMultiplier); // "sprinting"
		}
		return direction * acceleration; // "walking"
	}
}