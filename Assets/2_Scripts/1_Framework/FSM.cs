using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> A Finite State Machine that keeps track of state and includes a blackboard </summary>
/// <typeparam name="T"> BlackboardType </typeparam>
public class Fsm<T> : IUpdateable
{
	public BaseState<T> CurrentState { get; private set; }
	
	public Dictionary<System.Type, BaseState<T>> States { get; protected set; } = new Dictionary<System.Type,BaseState<T>>();
	
	public Fsm(T blackboard, params BaseState<T>[] newStates)
	{
		foreach (BaseState<T> baseState in newStates)
		{
			this.States.Add(baseState.GetType(), baseState);
			baseState.Init(this, blackboard);
		}
		
		SwitchState(newStates[0].GetType());
	}
	
	public void SwitchState(System.Type state)
	{
		CurrentState?.Exit();
		States.TryGetValue(state, out BaseState<T> baseState);
		CurrentState = baseState;
		CurrentState.Enter();
	}

	public void Update()
	{
		CurrentState?.Update();
	}
}
