using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boost : MonoBehaviour
{
    public float boostCooldown = 5f;
    public float boostDuration = 2f;
    private float speedBoost = 5;

    private bool hasCooldown;
    private Vector3 normalMovementVector = Vector3.forward;
    private Vector3 currentMovementVector;

    public Slider boostSlider;

    void Start()
    {
        currentMovementVector = normalMovementVector;

        // doesn't allow to have speed right at the beginning
        // but comment it out if you want to have boost immediately at startup
        StartCoroutine(ActivateCooldown());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !hasCooldown)
        {
            // apply boost, i simply added another vector to it
            currentMovementVector += Vector3.forward * speedBoost;
            // activate the cooldown and start the deactivation method for the boost
            StartCoroutine(ActivateCooldown());
            StartCoroutine(ResetMovementVector());
            boostSlider.value--;
        }
        // just some basic movement for the test
        transform.Translate(currentMovementVector * Time.deltaTime);
    }

    IEnumerator ResetMovementVector()
    {
        // wait some seconds
        yield return new WaitForSeconds(boostDuration);
        // return to normal speed
        currentMovementVector = normalMovementVector;
        boostSlider.minValue = 0;
        Debug.Log("boost ended");
    }

    IEnumerator ActivateCooldown()
    {
        // put some code to disable the boost-is-ready bar
        // diable the ability to use boost
        hasCooldown = true;
        // wait until the boost is ready again
        yield return new WaitForSeconds(boostCooldown);
        hasCooldown = false;
        boostSlider.maxValue = 10;
        Debug.Log("boost ready");
    }
}

