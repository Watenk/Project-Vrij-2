using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UnderwaterShader : MonoBehaviour
{
    public static event Action<int> OnWaterJump = delegate { };

    [Header("Depth Parameters")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private int depth = 1;

    [Header("Volumes")]
    public GameObject surfaceVolume;
    public GameObject underwaterVolume;

    //public GameObject DistortionPlane;
    private bool underWater = false;

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
            if(underWater)
            {
                underwaterVolume.SetActive(true);
                surfaceVolume.SetActive(false);
                OnWaterJump(1);
                underWater = !underWater;
            }
            //DistortionPlane.SetActive(true);
        }
        else
        {
            if (!underWater) {
                //RenderSettings.fog = false;
                underwaterVolume.SetActive(false);
                surfaceVolume.SetActive(true);
                OnWaterJump(1);
                underWater = !underWater;
            }
            //DistortionPlane.SetActive(false);
        }
    }
    

    
}
