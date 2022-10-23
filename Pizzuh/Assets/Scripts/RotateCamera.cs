using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateCamera : MonoBehaviour
{
	public InputAction mouseRot;
	public InputAction rot;

	[SerializeField]	Vector2 sensitivity = Vector2.one * 5f;
	[SerializeField]	Vector2 mouseSensitivity = Vector2.one * 5f;
	[SerializeField]	Vector2 autofocusSpeed = Vector2.one * 5f;
	[SerializeField]	float focusDelay = 2f;
	float rotX, rotY;
	[SerializeField]	float minX = -80;
	[SerializeField]	float maxX = 80;
	float inputDelay = 0f;

	Vector2 input = Vector2.zero;
	private void Awake() {
		rotX = rotY = 0f;
		rot.started += ctx => StartCoroutine(Rotate(ctx.ReadValue<Vector2>()));
		rot.performed += ctx => input = ctx.ReadValue<Vector2>();
		rot.canceled += ctx => input = Vector2.zero;

		mouseRot.performed += ctx => {
			if (Cursor.lockState != CursorLockMode.Locked || !enabled)	return;

			input = ctx.ReadValue<Vector2>();

			rotX = Mathf.Clamp(rotX + input.y * Time.deltaTime * mouseSensitivity.x, minX, maxX);

			rotY += input.x * Time.deltaTime * mouseSensitivity.y;
			if (rotY > 180f) rotY -= 360f;
			if (rotY < -180f) rotY += 360f;

			transform.localRotation = Quaternion.Euler(rotX, rotY, 0f);

			inputDelay = focusDelay;
		};

		Cursor.lockState = CursorLockMode.Locked;
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
		mouseRot.Enable();

		transform.localRotation = Quaternion.Euler(rotX, rotY, 0f);
	}

	private void OnDisable() {
		rot.Disable();
		mouseRot.Disable();

		transform.localRotation = Quaternion.identity;
	}

	IEnumerator Rotate(Vector2 val) {
		input = val;
		while (input != Vector2.zero) {
			//don't move if cursor isn't locked
			if (Cursor.lockState == CursorLockMode.Locked && enabled) {
				rotX = Mathf.Clamp(rotX + input.y * Time.deltaTime * sensitivity.x, minX, maxX);

				rotY += input.x * Time.deltaTime * sensitivity.y;
				if (rotY > 180f) rotY -= 360f;
				if (rotY < -180f) rotY += 360f;

				transform.localRotation = Quaternion.Euler(rotX, rotY, 0f);

				inputDelay = focusDelay;
			}
			yield return null;
		}
	}
}
