using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UnderwaterShader : MonoBehaviour
{
    [Header("Depth Parameters")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private int depth = 1;

    [Header("Volumes")]
    public GameObject surfaceVolume;
    public GameObject underwaterVolume;

    public GameObject DistortionPlane;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mainCamera.position.y < depth)
        {
            //RenderSettings.fog = true;
            underwaterVolume.SetActive(true);
            surfaceVolume.SetActive(false);
            DistortionPlane.SetActive(true);
        }
        else
        {
            //RenderSettings.fog = false;
            underwaterVolume.SetActive(false);
            surfaceVolume.SetActive(true);
            DistortionPlane.SetActive(false);
        }
    }
    

    
}
