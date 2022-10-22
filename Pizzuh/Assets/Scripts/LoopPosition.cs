using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPosition : MonoBehaviour
{
	public Transform target;
	public Transform[] otherTargets;
	public float maxX = 305f;
	public float minX = -305f;
	public float transposAmt = 600f;
	static public LoopPosition instance;

	private void Awake() {
		instance = this;
	}

	private void Update() {
		if (target.position.x > maxX) {
			target.position += Vector3.left * transposAmt;
			foreach (Transform trans in otherTargets) {
				trans.position += Vector3.left * transposAmt;
			}
		}
		if (target.position.x < minX) {
			target.position += Vector3.right * transposAmt;
			foreach (Transform trans in otherTargets) {
				trans.position += Vector3.right * transposAmt;
			}
		}
	}
}
