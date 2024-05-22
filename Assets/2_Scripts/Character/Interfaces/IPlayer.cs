using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
	public ICharacterInputHandler CharacterInputHandler { get; }
	public ICharacterMovement CharacterMovement { get; }
	public IAttack CharacterAttack { get; }
	public Health<IPlayer> CharacterHealth { get; }
}
