using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCloner : MonoBehaviour
{
	private void LateUpdate() {
		if (LoopPosition.instance.target.position.x - transform.position.x < LoopPosition.instance.transposAmt * -0.4f) {
				transform.position += Vector3.left * LoopPosition.instance.transposAmt;
		}
		if (LoopPosition.instance.target.position.x - transform.position.x > LoopPosition.instance.transposAmt * 0.4f) {
			transform.position += Vector3.right * LoopPosition.instance.transposAmt;
		}
	}
}
