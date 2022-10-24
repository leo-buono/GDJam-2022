using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
	public RectTransform UIElement;
	public TMPro.TMP_Text distanceText;
	[SerializeField]	Transform player;
	[SerializeField]	Enemy enemy;
	[SerializeField]	Camera cam;
	[SerializeField]	Vector2 boundsPercentY = Vector2.up * 0.8f;
	[SerializeField]	Vector2 boundsPercentX = Vector2.right * 0.2f + Vector2.up * 0.8f;
	[SerializeField]	Transform[] doors;
	[SerializeField]	TimeBar time;
	[SerializeField]	int scoreValue = 1000;

	private void Start() {
		GenWaypoint();
	}

	void GenWaypoint() {
		transform.position = doors[Random.Range(0, doors.Length)].position + Vector3.right * LoopPosition.instance.transposAmt;
		if (transform.position.x - player.position.x < LoopPosition.instance.transposAmt) {
			transform.position += Vector3.right * LoopPosition.instance.transposAmt;
		}
	}

	private void LateUpdate() {
		distanceText.transform.localRotation = Quaternion.Euler(0, 0, 360f -
			CalcWorldSpaceUI(UIElement, cam, transform.position, boundsPercentX, boundsPercentY));

		distanceText.text = Mathf.Floor(Vector3.Distance(transform.position, player.position)).ToString() + "m";
	}

	//returns angle
	static public float CalcWorldSpaceUI(RectTransform UIElement, Camera cam, Vector3 targetPos, Vector2 boundsPercentX, Vector2 boundsPercentY) {

		//forward check
		float val = 0f;
		UIElement.position = cam.WorldToScreenPoint(targetPos);
		if (Vector3.Dot(targetPos - cam.transform.position, cam.transform.forward) > 0f){
			UIElement.position = new Vector3(
				Mathf.Clamp(UIElement.position.x, 0f, Screen.width),
				Mathf.Clamp(UIElement.position.y, 0f, Screen.height),
				0f);

			//rotation is pointing outwards if far off screen
			if (UIElement.position.x < Screen.width * boundsPercentX.x ||
				UIElement.position.x > Screen.width * boundsPercentX.y ||
				UIElement.position.y < Screen.height * boundsPercentY.x ||
				UIElement.position.y > Screen.height * boundsPercentY.y) {

				val = 180f + Vector2.SignedAngle(Vector2.up, (Vector2)UIElement.position -
					(Vector2.right * Screen.width * 0.5f + Vector2.up * Screen.height * 0.5f));
				UIElement.localRotation = Quaternion.Euler(0f, 0f, val);
			}
			else {
				UIElement.localRotation = Quaternion.identity;
			}
		}
		else {
			//invert because backwards
			UIElement.position = new Vector3(Screen.width - UIElement.position.x, Screen.height - UIElement.position.y, 0f);
			//if already in bounds, clamp, else clip to bounds
			UIElement.position = new Vector3(
				(UIElement.position.y > 0f && UIElement.position.x < Screen.height) ?
				UIElement.position.x < Screen.width * 0.5f ? 0f : Screen.width :
				Mathf.Clamp(UIElement.position.x, 0f, Screen.width),

				(UIElement.position.x > 0f && UIElement.position.x < Screen.width) ?
				UIElement.position.y < Screen.height * 0.5f ? 0f : Screen.height :
				Mathf.Clamp(UIElement.position.y, 0f, Screen.height),

				0f);

			//always rotation
			val = 180f + Vector2.SignedAngle(Vector2.up, (Vector2)UIElement.position -
				(Vector2.right * Screen.width * 0.5f + Vector2.up * Screen.height * 0.5f));
			UIElement.localRotation = Quaternion.Euler(0f, 0f, val);
		}

		return val;
	}

	private void OnTriggerEnter(Collider other) {
		if (!other.CompareTag("Player"))	return;
		time.ResetClock();
		GenWaypoint();
		player.GetComponent<BicycleController>().enabled = true;

		enemy.Pushback();
		Score.AddScore(scoreValue);
		GetComponent<AudioSource>().Play();
	}
}
