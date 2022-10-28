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

	public GameObject popup;
	static Score instance;

    void Start()
    {
		instance = this;

        text = GetComponentInChildren<TextMeshProUGUI>();
		text.text = "Score : 0";
		fire.rectTransform.localScale = Vector3.zero;
    }
    static public void AddScore(int amount)
    {
        score += amount;

		instance.StartCoroutine(instance.Popup(amount));
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

	Transform laststack;

	IEnumerator Popup(int amt) {
		float counter = 5f;
		//render object here
		if (laststack == null) {
			laststack = transform;
		}
		GameObject msg = Instantiate(popup, transform);

		if (laststack != transform) {
			laststack.SetParent(msg.transform);
			laststack.localPosition = Vector3.up * 60f;
		}

		laststack = msg.transform;

		TMPro.TMP_Text txt = msg.GetComponent<TMPro.TMP_Text>();
		txt.text = "+" + amt.ToString();

		Color fade = txt.color;

		while (counter > 0) {
			counter -= Time.deltaTime;

			if (counter < 1) {
				fade.a = counter;
				txt.color = fade;
			}
			yield return null;
		}
		//hide object here
		Destroy(msg);
	}
}
