using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gamemanager : MonoBehaviour
{
	public InputAction startGame;
	public TimeBar time;
	[SerializeField]	float timeLimit = 30f;
	public BicycleController player;
	public Enemy enemy;
	public GameObject gameOverObj;

	private void Awake() {
		player.enabled = false;
		enemy.enabled = false;
		gameOverObj.SetActive(false);

		startGame.Enable();
		startGame.started += ctx => {
			time.StartGame(timeLimit);
			player.enabled = true;
			enemy.enabled = true;

			//kill it after starting
			startGame.Disable();
		};
	}

	public void StopGame() {
		player.enabled = false;
		//give prompt to reset?
		time.enabled = false;

		gameOverObj.SetActive(true);
	}
}
