using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCloner : MonoBehaviour
{
	private void Update() {
		if (LoopPosition.instance.target.position.x > 0f &&
			transform.position.x < LoopPosition.instance.transposAmt * -0.25f) {
				transform.position += Vector3.right * LoopPosition.instance.transposAmt;
		}
		if (LoopPosition.instance.target.position.x < 0f &&
			transform.position.x > LoopPosition.instance.transposAmt * 0.25f) {
			transform.position += Vector3.left * LoopPosition.instance.transposAmt;
		}
	}
}
