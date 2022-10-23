using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private InputAction g;
    private TextMeshProUGUI newText;
    private int score = 0;
    private bool addingScore = false;

    private void Awake()
    {
        g.started += ctx =>
        {
            AddScore(100);
        };
    }

    private void FixedUpdate()
    {
        if (addingScore)
        {
            newText.GetComponent<RectTransform>().position += new Vector3();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        text.text = score.ToString();

        newText = Instantiate(text, gameObject.transform);
        newText.color = Color.clear;
        addingScore = true;
    }
}
