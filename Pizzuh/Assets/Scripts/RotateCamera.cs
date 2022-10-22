using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateCamera : MonoBehaviour
{
	public InputAction rot;

	[SerializeField]	Vector2 sensitivity = Vector2.one * 5f;
	[SerializeField]	Vector2 autofocusSpeed = Vector2.one * 5f;
	[SerializeField]	float focusDelay = 2f;
	float rotX, rotY;
	[SerializeField]	float minX = -80;
	[SerializeField]	float maxX = 80;
	float inputDelay = 0f;

	Vector2 input;
	private void Awake() {
		rotX = rotY = 0f;
		rot.performed += ctx => {
			//don't move if curos isn't locked
			if (Cursor.lockState != CursorLockMode.Locked)	return;

			input = ctx.ReadValue<Vector2>();
			rotX = Mathf.Clamp(rotX + input.y * Time.deltaTime * sensitivity.x, minX, maxX);

			rotY += input.x * Time.deltaTime * sensitivity.y;
			if (rotY > 180f)	rotY -= 360f;
			if (rotY < -180f)	rotY += 360f;

			transform.localRotation = Quaternion.Euler(rotX, rotY, 0f);

			inputDelay = focusDelay;
		};

		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Start() {
		rotX = rotY = 0f;
		transform.rotation = Quaternion.identity;
	}

	private void Update() {
		if (inputDelay > 0) {
			inputDelay -= Time.deltaTime;
		}
		else if (rotX != 0f || rotY != 0f) {
			rotX = Mathf.MoveTowards(rotX, 0f, autofocusSpeed.x * Time.unscaledDeltaTime);
			rotY = Mathf.MoveTowards(rotY, 0f, autofocusSpeed.y * Time.unscaledDeltaTime);
			transform.localRotation = Quaternion.Euler(rotX, rotY, 0f);
		}
	}

	private void OnEnable() {
		rot.Enable();
	}

	private void OnDisable() {
		rot.Disable();
	}
}
