using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using Watenk;

public class Human : IGameObject, IFixedUpdateable
{
	public uint ID { get; private set; }
	public GameObject GameObject { get; private set; }
	
	private HumanAI humanAI;

	// Dependencies
	private HumansSettings humansSettings;
	private GameObject parent;
	private NavMeshSurface navMeshSurface;

	public Human(GameObject boat, HumansSettings humansSettings)
	{
		this.parent = boat;
		this.humansSettings = humansSettings;
		
		GameObject randomPrefab = humansSettings.HumanPrefabs[Random.Range(0, humansSettings.HumanPrefabs.Count)];
		if (randomPrefab == null) DebugUtil.ThrowError("RandomPrefab is null. The boatspawner probably doesn't have any boat prefabs assigned.");
		navMeshSurface = boat.GetComponent<NavMeshSurface>();
		if (navMeshSurface == null) DebugUtil.ThrowError("navMeshSurface is null. The boat probably doesn't have a navMeshSurface.");
		Vector3 randomPos = boat.transform.position;
		
		GameObject = GameObject.Instantiate(randomPrefab, randomPos, Quaternion.identity);
	}

	public void ChangeID(uint newID)
	{
		ID = newID;
	}

	public void FixedUpdate()
	{
	}
}
