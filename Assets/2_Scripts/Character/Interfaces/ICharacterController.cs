using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterMovement 
{
	public void UpdateRotation(Vector2 rotationInput);
	public void UpdateMovement(Vector2 moveInput, float verticalMoveInput);
	public void Boost();
}
