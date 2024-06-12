using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterMovementSettings", menuName = "Settings/Character/CharacterMovementSettings")]
public class CharacterMovementSettings : ScriptableObject
{
	public float Speed;
	public float Gravity;
	public float BoostStrenght;
	public float BoostCooldownLenght;
	public float LandMovementDebuff;
	public float RotationSensitivity;
	[Tooltip("The amount the camera will tilt by up and down movement")]
	public float CameraTiltIntencity;
}
