using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BicycleController : MonoBehaviour
{
	[SerializeField]	Rigidbody rb;
	[SerializeField]	InputAction rotate;
	[SerializeField]	InputAction accelerate;
	public float speed = 5f;
	[SerializeField]	Transform rotationPoint;
	[SerializeField]	Transform centerOfMass;
	[SerializeField]	float rotAngle = 15f;
	[SerializeField]    float rotForce = 5f;
	[SerializeField]	float fixForce = 5f;
	[SerializeField]	float maxSpeed = 30f;

	[HideInInspector]	public float accelVelo;

	[HideInInspector]	public float angularVelo = 1f;


	private void Awake() {
		rb.centerOfMass = centerOfMass.localPosition;

		angularVelo = 1f;
		rotate.performed += ctx => {
			float angle = ctx.ReadValue<float>();
			rotationPoint.localRotation = Quaternion.Euler(0f, angle * rotAngle, 0f);

			angularVelo = angle * rotForce;
		};
		rotate.canceled += ctx => {
			rotationPoint.localRotation = Quaternion.identity;

			angularVelo = 0f;
		};

		accelVelo = 1f;
		accelerate.performed += ctx => {
			accelVelo = (0.5f + ctx.ReadValue<float>());
		};
		accelerate.canceled += ctx => {
			accelVelo = 1f;
		};
	}

	private void OnEnable() {
		rotate.Enable();
		accelerate.Enable();
	}

	private void OnDisable() {
		rotate.Disable();
		accelerate.Disable();
	}

	float tilt;
	private void FixedUpdate() {
		tilt = Vector3.SignedAngle(Vector3.up, transform.up, transform.forward);
		if (Mathf.Abs(tilt) > 2f)
			rb.AddRelativeTorque((Vector3.forward * -tilt).normalized * fixForce);
		else
			rb.AddRelativeTorque(Vector3.forward * -tilt);

		//clamp speed
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
	}

	public float GetMaxSpeed() { return maxSpeed; }

}
