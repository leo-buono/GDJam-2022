using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float acceleration = 5f;
	public float maxSpeed = 15f;
	float speed = 0f;
	[SerializeField]	Gamemanager manager;
    // Update is called once per frame
    void Update()
    {
		if (speed < maxSpeed)
			speed = Mathf.MoveTowards(speed, maxSpeed, Time.deltaTime * acceleration);

        transform.position += Vector3.right * speed * Time.deltaTime;
    }

	private void OnCollisionEnter(Collision other) {
		if (!other.gameObject.CompareTag("Player")) return;

		manager.StopGame();
	}
}
