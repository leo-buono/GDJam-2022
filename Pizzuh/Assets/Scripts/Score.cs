using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    static public TextMeshProUGUI text;
    static public int score = 0;
    int displayScore = 0;
	public int multiplier = 3;
	public Image fire;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
		text.text = "Score : 0";
		fire.rectTransform.localScale = Vector3.zero;
    }
    static public void AddScore(int amount)
    {
        score += amount;
    }

	private void Update() {
		if (score != displayScore) {
			displayScore += multiplier;
			if (displayScore > score)
				displayScore = score;
        	text.text = "Score : " + displayScore.ToString();
			fire.rectTransform.localScale = Vector3.MoveTowards(fire.rectTransform.localScale, Vector3.one, Time.deltaTime * 5f);
		}
		else {
			fire.rectTransform.localScale = Vector3.MoveTowards(fire.rectTransform.localScale, Vector3.zero, Time.deltaTime * 2f);
		}
	}

	private void OnDestroy() {
		score = 0;
	}
}
