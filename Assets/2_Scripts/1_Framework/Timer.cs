using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : ITimer
{
    public event ITimer.TimerEventHandler OnTimer;

	public float TimeLeft { get { return timeLeft; } }
	protected float timeLeft;
	public float StartTime { get { return startTime; } }
	protected float startTime;

    public Timer(float startTime)
	{
		this.startTime = startTime;
		timeLeft = startTime;
	}

	public void Tick(float deltaTime)
	{
		timeLeft -= deltaTime;
		CheckTimerState();
	}
	
	public void ChangeStartTime(float newStartTime)
	{
		startTime = newStartTime;
	}
	
	public void Reset()
	{
		timeLeft = startTime;
	}
	
	protected virtual void CheckTimerState()
	{
		if (timeLeft <= 0)
		{
			OnTimer?.Invoke();
			timeLeft = 0;
		}
	}
}
