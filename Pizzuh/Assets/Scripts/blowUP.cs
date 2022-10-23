using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blowUP : MonoBehaviour
{
    public float additionalForce = 5f;
    private void OnCollisionEnter(Collision other)  
    {
        if(other.gameObject.tag == "Player")
            GetComponent<Rigidbody>().AddForce(other.impulse * additionalForce, ForceMode.Impulse);    
    }
}
