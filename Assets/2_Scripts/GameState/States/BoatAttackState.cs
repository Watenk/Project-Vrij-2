using UnityEngine;

public class BoatAttackState : BaseState<GameManager>
{
	private int boatsSunk = 0;
	
	public override void Enter()
	{
		ServiceLocator.Instance.Get<EventManager>().AddListener(Event.OnBoatSunk, OnBoatSunk);
	}

	public override void Exit()
	{
		ServiceLocator.Instance.Get<EventManager>().RemoveListener(Event.OnBoatSunk, OnBoatSunk);
	}

	private void OnBoatSunk()
	{
		boatsSunk++;
		if (boatsSunk == bb.GameSettings.BoatKillAmountBossTrigger)
		{
			owner.SwitchState(typeof(BossBattleState));
		}
	}
}