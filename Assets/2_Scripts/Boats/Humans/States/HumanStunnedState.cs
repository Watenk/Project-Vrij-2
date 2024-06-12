using UnityEngine;

public class HumanStunnedState : BaseState<Human>
{
	private Timer stunnedTimer;
	private EventManager events;

	public override void Init(Fsm<Human> owner, Human blackboard)
	{
		base.Init(owner, blackboard);
		stunnedTimer = new Timer(Random.Range(bb.humansSettings.StunnedTimeBounds.x, bb.humansSettings.StunnedTimeBounds.y), false);
		events = ServiceLocator.Instance.Get<EventManager>();
	}

	public override void Enter()
	{
		stunnedTimer.Reset();
		stunnedTimer.OnTimer += OnStunnedTimer;
		events.Invoke(Event.OnHumanStunned, bb);
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