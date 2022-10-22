using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Spedometer : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private Image spdHoop;
    [SerializeField] private BicycleController bike;
    [SerializeField] private float spdFillMax = 0.65f;
    [SerializeField] private float spdMult = 1.3f;
    [SerializeField] private float redZone = 0.85f;
    [SerializeField] private float bounceSpd = 1;
    private float speed = 0f;
    private float maxSpeed = 0f;
    private float fill = 0f;
    private bool isBouncing = false;
    private float bounceAmount = 0f;

    private void Awake()
    {
        maxSpeed = bike.GetMaxSpeed();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = rb.velocity.magnitude * spdMult;
        txt.text = Mathf.Floor(speed) + "mps";
        if (!isBouncing)
        {
            fill = speed / (maxSpeed * spdMult) * spdFillMax;
        }
        else
        {
            fill -= bounceSpd/100;
        }

        spdHoop.fillAmount = fill;

        float o = spdFillMax * redZone;
        if (fill > o && !isBouncing)
        {
            float a = (fill - o) / (spdFillMax - o);
            spdHoop.color = new Color(1f, Mathf.Lerp(1f,0f, a), Mathf.Lerp(1f,0f, a));
        }
        else if (!isBouncing)
        {
            spdHoop.color = Color.white;
        }

        if (fill > spdFillMax)
        {
            isBouncing = true;
            bounceAmount = Random.Range(0f, 0.08f);
        }
        else if (fill < spdFillMax - bounceAmount && isBouncing) 
        {
            isBouncing = false;
        }
    }
}
