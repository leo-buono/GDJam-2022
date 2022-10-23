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
    
    [Range(0.01f, 1f)]
    [SerializeField] public float bounceMax = 0.08f;
    private float speed = 0f;
    private float fill = 0f;
    private bool isBouncing = false;
    private float bounceAmount = 0f;


    // Update is called once per frame
    void FixedUpdate()
    {
        speed = rb.velocity.magnitude;
        if (!isBouncing)
        {
            fill = speed / bike.GetMaxSpeed() * spdFillMax;
        }
        else
        {
            fill -= bounceSpd/100f;
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

        if (fill >= spdFillMax)
        {
            isBouncing = true;
            bounceAmount = Random.Range(0.01f, bounceMax);
        }
        else if (fill < spdFillMax - bounceAmount && isBouncing) 
        {
            isBouncing = false;
        }


        if ((int)speed == (int)bike.GetMaxSpeed())
        {
            txt.text = Mathf.Floor(Random.Range(bike.GetMaxSpeed(), 99.99f)) + "m/s";
        }
        else
        {
            txt.text = Mathf.Floor(speed * spdMult) + "m/s";
        }
    }
}
