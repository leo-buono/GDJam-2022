using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDistance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] RectTransform imgTransform;

    private void FixedUpdate()
    {
        txt.text = "0 m";
    }
}
