using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoatMovement : MonoBehaviour
{
    NavMeshAgent nma = null;
    GameObject[] RandomPoint;
    int CurrentRandom;

    // Start is called before the first frame update
    void Start()
    {
        nma = this.GetComponent<NavMeshAgent>();
        RandomPoint = GameObject.FindGameObjectsWithTag("RandomPoint");
        Debug.Log("RandomPoints = " + RandomPoint.Length.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if(nma.hasPath == false)
        {
            CurrentRandom = Random.Range(0, RandomPoint.Length + 1);
            nma.SetDestination(RandomPoint[CurrentRandom].transform.position);
            Debug.Log("Moving to RandomPoint" + CurrentRandom.ToString());
        }
    }
}
