using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
	public CharacterController CharacterController { get; }
	public CharacterAttack CharacterAttack { get; }
	public CharacterHealth Characterhealth { get; }
}
