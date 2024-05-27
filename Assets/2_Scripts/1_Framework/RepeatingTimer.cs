using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingTimer : Timer
{
	public RepeatingTimer(float startTime) : base(startTime) {}

	protected override void CheckTimerState()
	{
		base.CheckTimerState();
		
		timeLeft = startTime;
	}
}
