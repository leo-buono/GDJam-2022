using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    static public TextMeshProUGUI text;
    static public int score = 0;
    int displayScore = 0;
	int multiplier = 3;
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
		text.text = "Score : 0";
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
		}
	}
}
