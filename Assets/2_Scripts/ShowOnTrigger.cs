using UnityEngine;
using UnityEngine.UI;

public class ShowOnTrigger : MonoBehaviour
{
    [Header("The GameObject with the Image to display on trigger")]
    public GameObject imageObject;

    private void OnTriggerEnter(Collider other)
    {
        if (imageObject != null)
        {
            imageObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (imageObject != null)
        {
            imageObject.SetActive(false);
        }
    }
}
