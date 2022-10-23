using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    static public TextMeshProUGUI text;
    static public int score = 0;
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();      
    }
    static public void AddScore(int amount)
    {
        score += amount;
        text.text = "Score : " + score.ToString();
    }
}
