using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToObject : MonoBehaviour
{
    [Header("Managers")]
    public CameraManager cameraManager;

    public GameObject sphere;
    public GameObject Cube;
    public float speed;

    void Update()
    {
        sphere.transform.position = Vector3.MoveTowards(sphere.transform.position, Cube.transform.position, speed);
    }
}
