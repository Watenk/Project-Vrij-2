using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoatSpawn : MonoBehaviour
{
    public GameObject[] myObjects;

    public float MinRange = -10;
    public float MaxRange = 11;

    public int spawnAmountMax = 10;
    private int spawned = 0;

    public float spawnTime = 0f;
    float spawnTimeLeft = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawned >= spawnAmountMax)
            return;

        if (spawnTimeLeft >= spawnTime)
        {
            int randomIndex = Random.Range(0, myObjects.Length);
            Vector3 randomSpawnPosition = new Vector3(Random.Range(MinRange, MaxRange), 0, Random.Range(MinRange, MaxRange));

            Instantiate(myObjects[randomIndex], randomSpawnPosition, Quaternion.identity);

            spawnTimeLeft = 0f;

            spawned++;
        }
        else
        {
            spawnTimeLeft = spawnTimeLeft + Time.deltaTime;
        }
    }
}
