using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] float speed;
    //private float time = 0f;
    
    void Update()
    {
        image.fillAmount -= Time.deltaTime * speed;
    }
}
