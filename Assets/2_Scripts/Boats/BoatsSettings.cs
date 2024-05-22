using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoatsSettings", menuName = "Settings/Boat/BoatsSettings")]
public class BoatsSettings : ScriptableObject
{
	[Tooltip("The min and max speed of the boats")]
	public Vector2 SpeedBounds;
	[Tooltip("The range the boat will be able to sail in")] 
	public byte sailingRange;
	public List<GameObject> BoatPrefabs = new List<GameObject>();
}
