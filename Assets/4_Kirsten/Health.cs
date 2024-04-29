using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider heathSlider;

    PLayerController PLayerController;

    // Start is called before the first frame update
    void Start()
    {
        heathSlider.value = 2;
        PLayerController = GetComponent<PLayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PLayerController.EatedFish == true)
        {
            heathSlider.value = 10;
            PLayerController.EatedFish = false;
        }
    }
}
