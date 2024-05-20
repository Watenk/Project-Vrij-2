using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private SwarmChannel swarmChannel;
	
	private void Awake() 
	{
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	public void FixedUpdate() 
	{
		swarmChannel.SwarmCollection.FixedUpdate();
	}
}
