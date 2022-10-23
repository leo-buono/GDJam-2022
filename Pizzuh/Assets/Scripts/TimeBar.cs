using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
	[SerializeField] Gamemanager manager;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI text;
	[SerializeField] float speed;
    private float time = 0f;
    private bool start = false;
    private float currenttime = 0f;

	private void Awake() {
    	text.text = "";
	}

    void Update()
    {
        if (start)
        {
            currenttime -= Time.deltaTime;

            //image.fillAmount = Mathf.Lerp(0f, 1f, currenttime / time);
            image.fillAmount = currenttime / time;

            if (currenttime > 0)
            {
                text.text = currenttime.ToString("0.00");
            }
            else
            {
                text.text = "Out of time!";
                // you lose! good day sir!
				manager.player.enabled = false;
            }
        }
    }

	public void StartGame(float duration) {
		start = true;
		currenttime = time = duration;
	}
	public void ResetClock() {
		currenttime = time;
	}

	public void LoseGame() {
		start = false;
		text.text = "You lose, pause to restart";
	}
}
