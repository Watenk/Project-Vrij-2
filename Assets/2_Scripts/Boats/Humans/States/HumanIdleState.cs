using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanIdleState : BaseState<Human>
{
	public Timer IdleTimer { get; private set; }

	public override void Init(Fsm<Human> owner, Human blackboard)
	{
		base.Init(owner, blackboard);
		IdleTimer = new Timer(1.0f);
	}

	public override void Enter()
	{
		base.Enter();
		IdleTimer.Reset();
	}

	public override void Update()
	{
		base.Update();
		IdleTimer.Tick(Time.deltaTime);
	}
}
