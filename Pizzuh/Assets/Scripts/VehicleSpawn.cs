using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawn : MonoBehaviour
{
    public List<GameObject> carList;
    public GameObject player;
    public Vector2 lowerRange;
    public Vector2 upperRange;
    public int carLimit = 10;
    public int carSpawn = 3;

    Queue<GameObject> cars = new Queue<GameObject>();
    Queue<GameObject> cars2 = new Queue<GameObject>();

    private bool isInLowerRange = false;
    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > lowerRange.y && isInLowerRange)
        {
            SpawnCar(isInLowerRange, cars);
            isInLowerRange = false;
        }
        else if (player.transform.position.x > lowerRange.x && player.transform.position.x < lowerRange.y && !isInLowerRange)
        {
            SpawnCar(isInLowerRange, cars2);
            isInLowerRange = true;
        }
    }

    void SpawnCar(bool rangeType, Queue<GameObject> cars)
    {
        for (int i = 0; cars.Count > 0; i++)
        {
            Destroy(cars.Dequeue());
        }
        for (int i = 0; i < carSpawn; i++)
        {
            int random = Random.Range(0, carList.Count);
            float randomPos = 0f;
            if(isInLowerRange)
                randomPos = Random.Range(lowerRange.x + 10, lowerRange.y - 10);
            else {
                randomPos = Random.Range(upperRange.x + 10, upperRange.y - 10);
            }
            cars.Enqueue(Instantiate(carList[random], new Vector3(randomPos, 2.5f, Random.Range(-3f, 3f)), Quaternion.identity));
        }
    }
}
