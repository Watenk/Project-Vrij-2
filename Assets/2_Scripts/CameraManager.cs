using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject[] cameras;

    public GameObject camToActivate;

    public void SwitchCamera()
    {
        foreach (GameObject cam in cameras)
        {
            cam.SetActive(false);
        }

        camToActivate.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Boat"))
        {
            SwitchCamera();
        }
    }
}
