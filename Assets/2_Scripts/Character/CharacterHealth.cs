using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

public class CharacterHealth : AHealth, IHealth
{
	public CharacterHealth(int maxHealth) : base(maxHealth) {}

	public override void Die()
	{
		DebugUtil.ThrowLog("Player Died!!");
	}
}
