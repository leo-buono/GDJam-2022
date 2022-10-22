using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamFov : MonoBehaviour
{
	[SerializeField]	Rigidbody rb;
	[SerializeField]	Cinemachine.CinemachineVirtualCamera cam;
	[SerializeField]	float maxSpeed = 50f;
	[SerializeField]	float minFov = 60f;
	[SerializeField]	float maxFov = 130f;
	[SerializeField]	float catchupSpeed = 50f;

	private void Start() {
		transform.SetParent(null);
	}

    void Update()
    {
        cam.m_Lens.FieldOfView = Mathf.MoveTowards(cam.m_Lens.FieldOfView, Mathf.Lerp(minFov, maxFov, rb.velocity.sqrMagnitude / (maxSpeed * maxSpeed)), Time.deltaTime * catchupSpeed);
    }
}
