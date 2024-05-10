using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoatSettings", menuName = "Settings/Boat/BoatSettings")]
public class BoatSettings : ScriptableObject
{
    public List<GameObject> BoatPrefabs = new List<GameObject>();
    public List<GameObject> HumanPrefabs = new List<GameObject>();
}
