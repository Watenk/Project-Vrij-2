using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private IServiceManager serviceManager;
	
	private void Awake() 
	{
		serviceManager.AddService(new SwarmManager());
	}

	private void Update()
	{
		serviceManager.Update();
	}
	
	private void FixedUpdate() 
	{
		serviceManager.FixedUpdate();
	}
}
