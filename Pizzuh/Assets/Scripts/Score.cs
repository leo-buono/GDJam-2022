using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    private int score = 0;

    public void AddScore(int amount)
    {
        score += amount;
        text.text = score.ToString();
    }
}
