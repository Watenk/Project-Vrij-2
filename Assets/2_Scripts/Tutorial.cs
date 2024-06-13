using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class Tutorial : MonoBehaviour
{
	public GameObject UIWasd;
	public GameObject UIMouse;
	public GameObject UIButtonUp;
	public GameObject UIButtonDown;
	public GameObject UIJumpBoat;
	public GameObject UIEatFish;
	public GameObject UIBoost;
	public GameObject UISing;

	private EventManager events;

	bool One;
	bool Two;
	bool SpacePressed;
	bool Boat;
	bool TriggerEnter;

	private void Start() 
	{
		events = ServiceLocator.Instance.Get<EventManager>();
		events.AddListener<GameObject>(Event.OnHumanGrabbed, (grabbedObject) => OnHumanGrabbed(grabbedObject));
		events.AddListener(Event.OnBoatSunk, () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
	}
	
	private void OnDestroy() {
		events.RemoveListener<GameObject>(Event.OnHumanGrabbed, (grabbedObject) => OnHumanGrabbed(grabbedObject));
		events.RemoveListener(Event.OnBoatSunk, () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
	}

	private void Update()
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
		}

		if (TriggerEnter == true) 
		{
			UIJumpBoat.SetActive(false);

			if (Input.GetKeyDown(KeyCode.LeftShift))
			{
				UIEatFish.SetActive(true);
				StartCoroutine(Coroutine());
			}
		}

		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			UIBoost.SetActive(false);
		}
	}

	private void OnHumanGrabbed(GameObject grabbedObject)
	{
		UIJumpBoat.SetActive(false);
		TriggerEnter = true;
		
	}

	private IEnumerator Coroutine()
	{
		yield return new WaitForSeconds(10);
		UIEatFish.SetActive(false);
		UISing.SetActive(true);
		StartCoroutine(Coroutine2());
	}

	private IEnumerator Coroutine2()
	{
		yield return new WaitForSeconds(10);
		UISing.SetActive(false);
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