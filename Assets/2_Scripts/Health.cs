using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider heathSlider;

    CharacterController PLayerController;

    void Start()
    {
        heathSlider.value = 2;
        PLayerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // if (PLayerController.EatedFish == true)
        // {
        //     heathSlider.value = 10;
        //     PLayerController.EatedFish = false;
        // }
    }
}
