using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimer
{
	public delegate void TimerEventHandler();
	public event TimerEventHandler OnTimer;
	public event TimerEventHandler OnTick;
	
	public float TimeLeft { get; }
	public float StartTime { get; }
	
	public void Tick(float deltaTime);
	public void ChangeStartTime(float newStartTime);
	public void Reset();
}
