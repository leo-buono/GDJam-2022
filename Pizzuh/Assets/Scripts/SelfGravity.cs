using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfGravity : MonoBehaviour
{
	Rigidbody rb;
	private void Start() {
		rb = GetComponent<Rigidbody>();
	}
	private void FixedUpdate() {
		rb.AddRelativeForce(Physics.gravity, ForceMode.Acceleration);
	}
}
