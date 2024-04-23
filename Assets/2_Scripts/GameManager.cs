using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private void Awake() 
	{
		ServiceManager.Instance.Add(new SwarmManager());
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
