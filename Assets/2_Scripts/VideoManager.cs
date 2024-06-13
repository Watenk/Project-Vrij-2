using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string videoUrl;
    public RawImage rawImage;
    public GameObject ImageRaw;
    IEnumerator Start()
    {
        if (videoPlayer == null || rawImage == null || string.IsNullOrEmpty(videoUrl))
            yield break;

        videoPlayer.url = videoUrl;
        videoPlayer.renderMode = VideoRenderMode.APIOnly;
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
            yield return new WaitForSeconds(1);

        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boat"))
        {
            ImageRaw.SetActive(true);
        }
    }
}