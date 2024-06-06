using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : ITimer
{
	public event ITimer.TimerEventHandler OnTimer;
	public event ITimer.TimerEventHandler OnTick;

	public float TimeLeft { get { return timeLeft; } }
	protected float timeLeft;
	public float StartTime { get { return startTime; } }
	protected float startTime;
	
	private bool running = true;

	public Timer(float startTime)
	{
		this.startTime = startTime;
		timeLeft = startTime;
	}
	
	public Timer(float startTime, bool running)
	{
		this.startTime = startTime;
		this.running = running;
		timeLeft = startTime;
	}

	public void Tick(float deltaTime)
	{
		if (!running) return;
		
		timeLeft -= deltaTime;
		CheckTimerState();
		OnTick?.Invoke();
	}
	
	public void ChangeStartTime(float newStartTime)
	{
		startTime = newStartTime;
	}
	
	public void Reset()
	{
		timeLeft = startTime;
		running = true;
	}
	
	protected virtual void CheckTimerState()
	{
		if (timeLeft <= 0)
		{
			OnTimer?.Invoke();
			timeLeft = 0;
			running = false;
		}
	}
}
