using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanIdleState : BaseState<Human>
{
	public Timer IdleTimer { get; private set; }

	public override void Init(Fsm<Human> owner, Human blackboard)
	{
		base.Init(owner, blackboard);
		IdleTimer = new Timer(Random.Range(bb.humansSettings.IdleTimeBounds.x, bb.humansSettings.IdleTimeBounds.y));
	}

	public override void Enter()
	{
		IdleTimer.Reset();
		IdleTimer.OnTimer += OnTimer;
	}

	public override void Exit()
	{
		IdleTimer.OnTimer -= OnTimer;
	}

	public override void Update()
	{
		IdleTimer.Tick(Time.deltaTime);
	}
	
	private void OnTimer()
	{
		owner.SwitchState(typeof(HumanWanderState));
	}
}
