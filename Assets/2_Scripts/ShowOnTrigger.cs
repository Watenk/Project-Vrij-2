using UnityEngine;

public class ShowOnTrigger : MonoBehaviour
{
    [Header("The GameObject with the Image to display on trigger")]
    public GameObject imageObject;

    [Header("The AudioSource to play on trigger")]
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (imageObject != null)
        {
            imageObject.SetActive(true);
            Debug.Log("Image activated");
        }
        if (audioSource != null)
        {
            // Play the audio from the beginning
            audioSource.Play();
            Debug.Log("Audio started playing");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (imageObject != null)
        {
            imageObject.SetActive(false);
            Debug.Log("Image deactivated");
        }
        // Do not stop the audio here to allow it to finish playing
    }
}
