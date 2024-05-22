using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public class TimerManager : MonoBehaviour
{
	private List<ITimer> timers = new List<ITimer>();
	
	public void Update()
	{
		foreach (ITimer timer in timers)
		{
			timer.Tick(Time.deltaTime);
		}
	}
	
	public ITimer Add(float startTime)
	{
		Timer timer = new Timer(startTime);
		timers.Add(timer);
		return timer;
	}
	
	public ITimer AddRepeating(float startTime)
	{
		RepeatingTimer timer = new RepeatingTimer(startTime);
		timers.Add(timer);
		return timer;
	}
	
	public void Remove(ITimer timer)
	{
		timers.Remove(timer);
	}
}