using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CamFov : MonoBehaviour
{
	public InputAction firstPerson;
	[SerializeField]	Rigidbody rb;
	CinemachineBrain camBrain;
	[SerializeField]	CinemachineVirtualCamera cam;
	[SerializeField]	CinemachineVirtualCamera cam2;
	[SerializeField]	RotateCamera followObj;
	[SerializeField]	float maxSpeed = 50f;
	[SerializeField]	float minFov = 60f;
	[SerializeField]	float maxFov = 130f;
	[SerializeField]	float catchupSpeed = 50f;

	private void Awake() {
		camBrain = Camera.main.GetComponent<CinemachineBrain>();
		firstPerson.started += ctx => {
			cam2.m_Priority = 100;
			followObj.enabled = false;
		};
		firstPerson.canceled += ctx => {
			cam2.m_Priority = 0;
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
		cam.transform.SetParent(null);
		cam2.transform.SetParent(null);
	}

    void Update()
    {
        cam.m_Lens.FieldOfView = Mathf.MoveTowards(cam.m_Lens.FieldOfView, Mathf.Lerp(minFov, maxFov, rb.velocity.sqrMagnitude / (maxSpeed * maxSpeed)), Time.deltaTime * catchupSpeed);
    }
}
