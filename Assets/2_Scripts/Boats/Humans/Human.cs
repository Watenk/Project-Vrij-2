using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Watenk;

public class Human : IGameObject, IFixedUpdateable
{
	public uint ID { get; private set; }
	public GameObject GameObject { get; private set; }
	
	// Dependencies
	private HumansSettings humansSettings;
	private GameObject parent;

	public Human(GameObject boat, HumansSettings humansSettings, Vector3 spawnPos)
	{
		this.parent = boat;
		this.humansSettings = humansSettings;
		
		GameObject randomPrefab = humansSettings.HumanPrefabs[Random.Range(0, humansSettings.HumanPrefabs.Count)];
		if (randomPrefab == null) DebugUtil.ThrowError("RandomPrefab is null. The boatspawner probably doesn't have any boat prefabs assigned.");
		
		GameObject = GameObject.Instantiate(randomPrefab, spawnPos, Quaternion.identity, boat.transform);
	}

	public void ChangeID(uint newID)
	{
		ID = newID;
	}

	public void FixedUpdate()
	{
		
	}
}
