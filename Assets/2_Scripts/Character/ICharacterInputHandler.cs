using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterInputHandler : IUpdateable
{
	public delegate void MoveEventHandler(Vector2 moveInput, float verticalMoveInput);
	public event MoveEventHandler OnMove;
	public delegate void RotateEventHandler(Vector2 rotationInput);
	public event RotateEventHandler OnRotate;
}
