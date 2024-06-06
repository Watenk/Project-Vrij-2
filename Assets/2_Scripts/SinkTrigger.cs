using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkTrigger : MonoBehaviour
{
    public BoatHandler handler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Human"))
        {
            if (!handler.fallenHumans.Contains(other.gameObject))
            {
                handler.fallenHumans.Add(other.gameObject);
                other.transform.parent = null;
            }
        }
    }
}
