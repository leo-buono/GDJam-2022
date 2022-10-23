using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onCollisionScoreAdd : MonoBehaviour
{
    public int scoreAmount = 0;
    public AudioSource sound;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && tag != "Used") 
        {
            //add score
            Score.AddScore(scoreAmount);
            //Do poppup
            sound.Play();
            tag = "Used";
        }
    }
}
