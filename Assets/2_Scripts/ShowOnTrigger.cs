using UnityEngine;

public class ShowOnTrigger : MonoBehaviour
{
    [Header("The GameObject with the Image to display on trigger")]
    public GameObject imageObject;

    [Header("The AudioSource to play on trigger")]
    public AudioSource audioSource;

    public float DisableTime { get { return disableTime; } }
    [SerializeField]
    private float disableTime;

    private Timer disableTimer;

    private void Start()
    {
        disableTimer = new Timer(DisableTime);
        disableTimer.OnTimer += OnDisableTimer;
    }

    private void Update()
    {
        disableTimer.Tick(Time.deltaTime);
    }

    private void OnDisable()
    {
        disableTimer.OnTimer -= OnDisableTimer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
        disableTimer.Reset();

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
    private void OnDisableTimer()
    {
        if (imageObject != null)
        {
            imageObject.SetActive(false);
            Debug.Log("Image deactivated");
        }
        // Do not stop the audio here to allow it to finish playing
    }
}
