using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CamFov : MonoBehaviour
{
	public InputAction firstPerson;
	[SerializeField]	Rigidbody rb;
	[SerializeField]	CinemachineVirtualCamera cam;
	Cinemachine3rdPersonFollow follow;
	[SerializeField]	RotateCamera followObj;
	[SerializeField]	float maxSpeed = 50f;
	[SerializeField]	float minFov = 60f;
	[SerializeField]	float maxFov = 130f;
	[SerializeField]	float catchupSpeed = 50f;
	[SerializeField]	float followDistance = 5f;

	private void Awake() {
		follow = cam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
		firstPerson.started += ctx => {
			follow.CameraDistance = 0.5f;
			followObj.enabled = false;
		};
		firstPerson.canceled += ctx => {
			follow.CameraDistance = followDistance;
			followObj.enabled = true;
		};
	}

	private void OnEnable() {
		firstPerson.Enable();
	}

	private void OnDisable() {
		firstPerson.Disable();
	}

	private void Start() {
		transform.SetParent(null);
	}

    void Update()
    {
        cam.m_Lens.FieldOfView = Mathf.MoveTowards(cam.m_Lens.FieldOfView, Mathf.Lerp(minFov, maxFov, rb.velocity.sqrMagnitude / (maxSpeed * maxSpeed)), Time.deltaTime * catchupSpeed);
    }
}
