using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	public GameSettings GameSettings { get; private set; }
	
	private Fsm<GameManager> gameState;
	
	private void Awake() 
	{
		Cursor.lockState = CursorLockMode.Locked;
		
		gameState = new Fsm<GameManager>(this,
			new BoatAttackState()
		);
	}
	
	public void FixedUpdate() 
	{
		
	}
}
