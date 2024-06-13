using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBattleState : BaseState<GameManager>
{
	public override void Enter()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}