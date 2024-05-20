using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> BaseState for FSM use. With a blackboard </summary>
/// <typeparam name="T"> BlackboardType </typeparam>
public abstract class BaseState<T> : IUpdateable where T : ScriptableObject
{
	protected Fsm<T> owner;
	protected T blackboard;
	
	public virtual void Init(Fsm<T> owner, T blackboard)
	{
		this.owner = owner;
		this.blackboard = blackboard;
	}
	
	public virtual void Enter() {}
	public virtual void Exit() {}
	public virtual void Update() {}
}
