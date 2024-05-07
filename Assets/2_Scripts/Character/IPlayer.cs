using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
	public ICharacterInputHandler CharacterInputHandler { get; }
	public ICharacterController CharacterController { get; }
	public IAttack CharacterAttack { get; }
	public IDamageable Characterhealth { get; }
}
