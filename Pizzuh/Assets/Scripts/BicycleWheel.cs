using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BicycleWheel : MonoBehaviour
{
	[SerializeField]	Rigidbody bikeBod;
	[SerializeField]	BicycleController bike;

	public bool steering = false;
	public bool controlled = false;

	Vector3 force = Vector3.zero;
	private void OnTriggerStay(Collider other) {
		if (controlled)
			bikeBod.AddForce(transform.forward * bike.speed * bike.accelVelo, ForceMode.Acceleration);
		else
			bikeBod.AddForce(transform.forward * bike.speed, ForceMode.Acceleration);
			
		if (steering)
			bikeBod.AddRelativeTorque(Vector3.up * bike.angularVelo, ForceMode.Acceleration);
	}
}
