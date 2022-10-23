using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onCollisionScoreAdd : MonoBehaviour
{
    public int scoreAmount = 0;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && tag != "Used") 
        {
            //add score
            tag = "Used";
        }
    }
}
