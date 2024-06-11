using UnityEngine;

public class HumanStunnedState : BaseState<Human>
{
	private Timer stunnedTimer;

	public override void Init(Fsm<Human> owner, Human blackboard)
	{
		base.Init(owner, blackboard);
		stunnedTimer = new Timer(Random.Range(bb.humansSettings.StunnedTimeBounds.x, bb.humansSettings.StunnedTimeBounds.y), false);
	}

	public override void Enter()
	{
		stunnedTimer.Reset();
		stunnedTimer.OnTimer += OnStunnedTimer;
	}

	public override void Exit()
	{
		stunnedTimer.OnTimer -= OnStunnedTimer;
	}

	private void OnStunnedTimer()
	{
		owner.SwitchState(typeof(HumanIdleState));
	}
}