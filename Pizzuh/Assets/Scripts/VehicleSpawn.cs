using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawn : MonoBehaviour
{
    public List<GameObject> carList;
    public GameObject player;
    public Vector2 lowerRange;
    public Vector2 upperRange;

    private bool isInLowerRange = false;
    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > lowerRange.y && isInLowerRange)
        {
            isInLowerRange = false;
        }
        else if (player.transform.position.x > lowerRange.x && player.transform.position.x < lowerRange.y && !isInLowerRange)
        {
            isInLowerRange = true;
        }
    }

    void SpawnCar(bool rangeType)
    {
        //Instatiate();
    }
}
