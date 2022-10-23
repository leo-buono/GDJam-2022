using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BicycleWheel : MonoBehaviour
{
	public bool disabled = false;
	[SerializeField]	Rigidbody bikeBod;
	[SerializeField]	BicycleController bike;
	[SerializeField]	Transform wheel;

	public bool steering = false;
	public bool controlled = false;
	public bool autoAccel = true;

	[SerializeField]	float acceleration = 200f;
	[SerializeField]	float decceleration = 500f;
	[SerializeField]	float maxSpeed = 720f;
	float wheelSpeed = 0f;
	bool moving = false;

	Vector3 force = Vector3.zero;
	private void OnTriggerStay(Collider other) {
		if (disabled || !bike.enabled)	return;

		if (autoAccel) {
			if (controlled && bike.accelVelo != 0f) {
				bikeBod.AddForce(transform.forward * bike.speed * (0.5f + bike.accelVelo), ForceMode.Acceleration);

				wheelSpeed = Mathf.Clamp(wheelSpeed + Time.fixedDeltaTime * acceleration * (0.5f + bike.accelVelo), -maxSpeed, maxSpeed);
			}
			else {
				bikeBod.AddForce(transform.forward * bike.speed, ForceMode.Acceleration);

				wheelSpeed = Mathf.Clamp(wheelSpeed + Time.fixedDeltaTime * acceleration, -maxSpeed, maxSpeed);
			}

			moving = true;
		}
		//if not auto, then only do something if controlled
		else if (controlled && bike.accelVelo != 0f) {
			bikeBod.AddForce(transform.forward * bike.speed * bike.accelVelo * 2f, ForceMode.Acceleration);

			wheelSpeed = Mathf.Clamp(wheelSpeed + Time.fixedDeltaTime * acceleration * bike.accelVelo, -maxSpeed, maxSpeed);

			moving = true;
		}
			
		if (steering) {
			if (bike.accelVelo >= 0)
				bikeBod.AddRelativeTorque(Vector3.up * bike.angularVelo, ForceMode.Acceleration);
			else
				bikeBod.AddRelativeTorque(Vector3.down * bike.angularVelo, ForceMode.Acceleration);
		}
	}

	private void Update() {
		if (wheelSpeed != 0f) {
			wheel.localRotation = Quaternion.Euler(wheelSpeed * Time.deltaTime, 0f, 0f) * wheel.localRotation;
		}
	}

	private void FixedUpdate() {
		if (!moving) {
			wheelSpeed = Mathf.MoveTowards(wheelSpeed, 0f, Time.fixedDeltaTime * decceleration);
		}
		moving = false;
	}
}
