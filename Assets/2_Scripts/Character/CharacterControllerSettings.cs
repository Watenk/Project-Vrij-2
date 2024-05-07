using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterControllerSettings", menuName = "Settings/CharacterControllerSettings")]
public class CharacterControllerSettings : ScriptableObject
{
	public float Speed;
	[Tooltip("The amount the camera will tilt by up and down movement")]
	public float CameraTiltIntencity;
}
