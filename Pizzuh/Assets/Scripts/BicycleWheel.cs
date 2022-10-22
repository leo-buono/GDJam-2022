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

	[SerializeField]	float acceleration = 200f;
	[SerializeField]	float decceleration = 500f;
	[SerializeField]	float maxSpeed = 720f;
	float wheelSpeed = 0f;
	bool moving = false;

	Vector3 force = Vector3.zero;
	private void OnTriggerStay(Collider other) {
		if (disabled)	return;

		if (controlled) {
			bikeBod.AddForce(transform.forward * bike.speed * bike.accelVelo, ForceMode.Acceleration);

			wheelSpeed = Mathf.Min(wheelSpeed + Time.fixedDeltaTime * acceleration * bike.accelVelo, maxSpeed);
		}
		else {
			bikeBod.AddForce(transform.forward * bike.speed, ForceMode.Acceleration);

			wheelSpeed = Mathf.Min(wheelSpeed + Time.fixedDeltaTime * acceleration, maxSpeed);
		}
			
		if (steering)
			bikeBod.AddRelativeTorque(Vector3.up * bike.angularVelo, ForceMode.Acceleration);
		
		moving = true;
	}

	private void FixedUpdate() {
		if (wheelSpeed > 0f) {
			wheel.localRotation = Quaternion.Euler(wheelSpeed * Time.fixedDeltaTime, 0f, 0f) * wheel.localRotation;

			if (!moving) {
				wheelSpeed -= Time.fixedDeltaTime * decceleration;
			}
			moving = false;

			if (wheelSpeed < 0f)
				wheelSpeed = 0f;
		}
	}
}
