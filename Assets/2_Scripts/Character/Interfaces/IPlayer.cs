using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
	public delegate void HealthChangeEventHandler(int amount);
	public event HealthChangeEventHandler ChangeHealth;
	
	public ICharacterInputHandler CharacterInputHandler { get; }
	public ICharacterMovement CharacterMovement { get; }
	public IAttack CharacterAttack { get; }
	public IHealth<IPlayer> CharacterHealth { get; }
	public ICharacterUI CharacterUI { get; }
}
