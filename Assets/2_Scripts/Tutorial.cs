using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject UIWasd;
    public GameObject UIMouse;
    public GameObject UIButtonUp;
    public GameObject UIButtonDown;
    public GameObject UIJumpBoat;
    public GameObject UIDrowning;
    public GameObject UIEatFish;
    public GameObject UIBoost;
    
    public GameObject BoatSpawn;

    bool One;
    bool Two;
    bool SpacePressed;
    bool Boat;
    bool TriggerEnter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControlsMouse();

        if (One == true && Two == true)
        {
            ButtonUp();
            UIButtonUp.SetActive(true);
        }

        if(SpacePressed == true)
        {
            UIButtonDown.SetActive(true);
            UIButtonUp.SetActive(false);
            ButtonDown();
        }

        if(Boat == true)
        {
            UIButtonDown.SetActive(false);
            UIJumpBoat.SetActive(true);
            BoatSpawn.SetActive(true);
        }

        if (TriggerEnter == true) 
        {
            UIJumpBoat.SetActive(false);
            Debug.Log("Boat is hit");

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                UIDrowning.SetActive(false);
                UIEatFish.SetActive(true);
                StartCoroutine(Coroutine());

            }

        }

        //if(Input.GetKeyDown (KeyCode.))
        //{
             //UIBoost.SetActive(false);
        //}
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(10);
        UIEatFish.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
            UIJumpBoat.SetActive(false);
            BoatSpawn.SetActive(false);
            UIDrowning.SetActive(true);
            TriggerEnter = true;

    }

    private void ButtonDown()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            UIButtonDown.SetActive(false);
            Boat = true;

        }
    }

    private void ButtonUp()
    {
         if(Input.GetKeyDown(KeyCode.Space))
         {
             UIButtonUp.SetActive(false);
             SpacePressed = true;
         }

    }

    private void ControlsMouse()
    {
        if(Input.GetAxis("Vertical") > .1f || Input.GetAxis("Horizontal") > .1f)
        {
            UIWasd.SetActive(false);
            One = true;
        }

        if(Input.GetAxis("Mouse Y") > .1f || Input.GetAxis("Mouse X") > .1f)
        {
            UIMouse.SetActive(false);
            Two = true;
        }

    }
}
