using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float acceleration = 5f;
	public float maxSpeed = 20f;
	public float minDistance = 25f;
	public float farSpeed = 35f;
	float speed = 0f;
	float realMaxSpeed = 0f;
	[SerializeField]	Gamemanager manager;
	[SerializeField]	Transform body;
	Rigidbody rb;
	Vector3 offset;
	private void Awake() {
		realMaxSpeed = maxSpeed;
		rb = GetComponent<Rigidbody>();
		offset = body.localPosition;
	}

    // Update is called once per frame
    void Update()
    {
		if (manager.player.transform.position.x - transform.position.x < minDistance)
			realMaxSpeed = maxSpeed;
		else
			realMaxSpeed = farSpeed;

		if (speed != realMaxSpeed)
			speed = Mathf.MoveTowards(speed, realMaxSpeed, Time.deltaTime * acceleration);
		
		rb.velocity = Vector3.right * speed;

		body.localPosition = Vector3.forward * manager.player.transform.position.z + offset;
    }

	private void OnDisable() {
		rb.velocity = Vector3.zero;
	}

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player"))
			manager.StopGame();
	}
}
