using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDistance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt;
	[SerializeField] Transform player;
	[SerializeField] Transform enemy;
	[SerializeField] Vector2 boundsPercentY = Vector2.up * 0.8f;
	[SerializeField] Vector2 boundsPercentX = Vector2.right * 0.2f + Vector2.up * 0.8f;

    private void FixedUpdate()
    {
		txt.transform.localRotation = Quaternion.Euler(0, 0, 360f -
			Waypoint.CalcWorldSpaceUI((RectTransform)transform, Camera.main, enemy.position, boundsPercentX, boundsPercentY));

		txt.text = Mathf.Floor(Vector3.Distance(enemy.position, player.position)).ToString() + "m";
    }
}
