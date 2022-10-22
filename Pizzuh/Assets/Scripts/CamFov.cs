using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFov : MonoBehaviour
{
	[SerializeField]	Rigidbody rb;
	[SerializeField]	Camera cam;
	[SerializeField]	float maxSpeed = 50f;
	[SerializeField]	float minFov = 60f;
	[SerializeField]	float maxFov = 130f;
	[SerializeField]	float catchupSpeed = 50f;

    void Update()
    {
        cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, Mathf.Lerp(minFov, maxFov, rb.velocity.sqrMagnitude / (maxSpeed * maxSpeed)), Time.deltaTime * catchupSpeed);
    }
}
