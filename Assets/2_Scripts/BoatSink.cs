using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSink : MonoBehaviour
{
    public GameObject boat;
    public GameObject Cube;
    public float speed;

    void Update()
    {
        boat.transform.position = Vector3.MoveTowards(boat.transform.position, Cube.transform.position, speed);
    }
}
