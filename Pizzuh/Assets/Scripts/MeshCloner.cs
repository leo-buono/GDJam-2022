using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCloner : MonoBehaviour
{
	Transform clone1;
	Transform clone2;
	[SerializeField]	GameObject meshPrefab;
	Vector3 lastPos;
	Quaternion lastRot;

	private void Start() {
		clone1 = Instantiate(meshPrefab, transform.position + Vector3.right * LoopPosition.instance.transposAmt, transform.rotation).transform;
		clone2 = Instantiate(meshPrefab, transform.position + Vector3.left * LoopPosition.instance.transposAmt, transform.rotation).transform;

		lastPos = transform.position;
		lastRot = transform.rotation;
	}

	private void Update() {
		if (transform.position.x > LoopPosition.instance.maxX) {
			transform.position += Vector3.left * LoopPosition.instance.transposAmt;
		}
		if (transform.position.x < LoopPosition.instance.minX) {
			transform.position += Vector3.right * LoopPosition.instance.transposAmt;
		}

		if (lastPos != transform.position) {
			clone1.position = transform.position + Vector3.right * LoopPosition.instance.transposAmt;
			clone2.position = transform.position + Vector3.left * LoopPosition.instance.transposAmt;

			lastPos = transform.position;
		}
		if (lastRot != transform.rotation) {
			clone1.rotation = clone2.rotation = transform.rotation;

			lastRot = transform.rotation;
		}
	}
}
