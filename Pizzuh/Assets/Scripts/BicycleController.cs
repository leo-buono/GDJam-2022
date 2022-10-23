using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BicycleController : MonoBehaviour
{
	[SerializeField]	Rigidbody rb;
	[SerializeField]	InputAction rotate;
	[SerializeField]	InputAction accelerate;
	[SerializeField]	InputAction jump;

	public float speed = 5f;
	[SerializeField]	Transform rotationPoint;
	[SerializeField]	Transform centerOfMass;
	[SerializeField]	float rotAngle = 15f;
	[SerializeField]    float rotForce = 5f;
	[SerializeField]    float rotSpeed = 20f;
	[SerializeField]	float fixForce = 5f;
	[SerializeField]	float jumpForce = 5f; //Get it? Because the game
	[SerializeField]	float maxSpeed = 30f;

	public float accelVelo = 0f;

	public float angularVelo = 0f;
	float targetAngularVelo = 0f;


	private void Awake() {
		rb.centerOfMass = centerOfMass.localPosition;

		angularVelo = 0f;
		rotate.started += ctx => {
			if (targetAngularVelo == angularVelo)
				StartCoroutine(RotateTire(ctx.ReadValue<float>() * rotForce));
		};
		rotate.performed += ctx => targetAngularVelo = ctx.ReadValue<float>() * rotForce;
		rotate.canceled += ctx => targetAngularVelo = 0f;

		accelVelo = 0f;
		accelerate.performed += ctx => accelVelo = ctx.ReadValue<float>();
		accelerate.canceled += ctx => accelVelo = 0f;

		jump.started += ctx => 
		{
			//jumping and stuff like that (it's relative to the direction of the player)
			if (groundedCount > 0)
				rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
		};
	}

	IEnumerator RotateTire(float newTarget) {
		targetAngularVelo = newTarget;
		float div = rotAngle / rotForce;
		while (targetAngularVelo != 0f || targetAngularVelo != angularVelo) {
			angularVelo = Mathf.MoveTowards(angularVelo, targetAngularVelo, Time.deltaTime * rotSpeed);

			rotationPoint.localRotation = Quaternion.Euler(0f, angularVelo * div, 0f);
			yield return null;
		}
		rotationPoint.localRotation = Quaternion.identity;
	}

	private void OnEnable() {
		rotate.Enable();
		accelerate.Enable();
		jump.Enable();
	}

	private void OnDisable() {
		rotate.Disable();
		accelerate.Disable();
		jump.Disable();

	}

	float tilt;
	private void FixedUpdate() {
		//side tilt
		tilt = Vector3.SignedAngle(Vector3.up, transform.up, transform.forward);
		if (Mathf.Abs(tilt) > 2f)
			rb.AddRelativeTorque((Vector3.forward * -tilt).normalized * fixForce);
		else
			rb.AddRelativeTorque(Vector3.forward * -tilt);

		//front tilt
		tilt = Vector3.SignedAngle(Vector3.up, transform.up, transform.right);
		if (Mathf.Abs(tilt) > 2f)
			rb.AddRelativeTorque((Vector3.right * -tilt).normalized * fixForce);
		else
			rb.AddRelativeTorque(Vector3.right * -tilt);

		//clamp speed
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
	}

	public float GetMaxSpeed() { return maxSpeed; }

	int groundedCount = 0;
	private void OnCollisionEnter(Collision other) {
		++groundedCount;
	}

	private void OnCollisionExit(Collision other) {
		--groundedCount;
	}
}
