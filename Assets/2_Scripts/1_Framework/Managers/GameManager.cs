using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private SwarmCollection swarmCollection = new SwarmCollection();
	
	private void Awake() 
	{
		Cursor.lockState = CursorLockMode.Locked;
		
		ServiceManager.Instance.Add(swarmCollection);
	}

	private void Update()
	{
		ServiceManager.Instance.Update();
	}
	
	private void FixedUpdate() 
	{
		ServiceManager.Instance.FixedUpdate();
	}
}
