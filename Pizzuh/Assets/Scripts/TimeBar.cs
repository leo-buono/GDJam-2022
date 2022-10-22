using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float speed;
    public float time = 10f;
    private bool start = false;
    private float currenttime = 0f;

    private void Start()
    {
        start = true;
        currenttime = time;
    }

    void Update()
    {
        if (start)
        {
            currenttime -= Time.deltaTime;
            image.fillAmount = Mathf.Lerp(0f, 1f, currenttime / time);

            if (currenttime >= -1)
            {
                text.text = Mathf.Floor(currenttime+1).ToString();
            }
            else
            {
                // you lose! good day sir!
            }
        }
    }
}
