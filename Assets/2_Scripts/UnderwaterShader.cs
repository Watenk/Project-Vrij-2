using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UnderwaterShader : MonoBehaviour
{
    [Header("Depth Parameters")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private int depth = 0;

    [Header("Volumes")]
    public GameObject surfaceVolume;
    public GameObject underwaterVolume;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mainCamera.position.y < depth)
        {
            RenderSettings.fog = true;
            underwaterVolume.SetActive(true);
            surfaceVolume.SetActive(false);
        }
        else
        {
            RenderSettings.fog = false;
            underwaterVolume.SetActive(false);
            surfaceVolume.SetActive(true);
        }
    }
    

    
}
