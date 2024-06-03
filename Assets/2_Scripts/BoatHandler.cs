using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatHandler : MonoBehaviour
{
    public BoatSink sinkBoat;
    public List<GameObject> fallenHumans;
    public List<GameObject> totalHumans;

    void Update()
    {
        if (fallenHumans.Count == totalHumans.Count)
        {
            sinkBoat.enabled = true;
            this.enabled = false;
        }
    }
}
