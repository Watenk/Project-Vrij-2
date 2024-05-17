using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HumansSettings", menuName = "Settings/Boat/HumansSettings")]
public class HumansSettings : ScriptableObject
{
	[Tooltip("The min and max amount of humans on the boats")]
	public Vector2Int HumanBounds;
	[Tooltip("The amount of space there will be between humans")]
	public float SperationDistance;
	public List<GameObject> HumanPrefabs = new List<GameObject>();
}
