using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public GameSettings GameSettings { get { return gameSettings; } }
	[SerializeField]
	private GameSettings gameSettings;
	private PlayerInputs inputs;
	private InputAction skip;
	
	private Fsm<GameManager> gameState;
	
	private void Awake() 
	{
		Cursor.lockState = CursorLockMode.Locked;
		
		gameState = new Fsm<GameManager>(this,
			new BoatAttackState(),
			new BossBattleState()
		);
		
		if (inputs == null)
		{
			inputs = new PlayerInputs();
			skip = inputs.Player.Skip;	
		}
		
		skip.Enable();
		skip.performed += context => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
	
	private void OnDestroy() 
	{
		skip.Disable();
		skip.performed -= context => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
