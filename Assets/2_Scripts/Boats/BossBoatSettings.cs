using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossBoatSettings", menuName = "Settings/Boat/BoatsSettings")]
public class BossBoatSettings : ScriptableObject
{
	[Tooltip("The min and max speed of the boats")]
	public Vector2 SpeedBounds;
	[Tooltip("The range the boat will be able to sail in")] 
	public byte sailingRange;
	public byte sinkSpeed;
	public byte sinkTime;
	public List<GameObject> BossBoat;
}
