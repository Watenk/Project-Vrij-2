using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Watenk;

public class Boat : IGameObject, IFixedUpdateable
{
	public uint ID { get; private set;}
	public GameObject GameObject { get; private set; }

	private DictCollectionFixedUpdate<Human> humanCollection = new DictCollectionFixedUpdate<Human>();
	private float speed;
	private int sailPointIndex;
	
	// Dependencies
	private List<Transform> sailPoints;
	private BoatsSettings boatSettings;
	private NavMeshAgent agent;
	private Transform orgin;
	
	public Boat(BoatsSettings boatSettings, HumansSettings humansSettings, Transform orgin, List<Transform> sailPoints)
	{
		this.boatSettings = boatSettings;
		this.sailPoints = sailPoints;
		this.orgin = orgin;
		sailPointIndex = Random.Range(0, sailPoints.Count - 1);
		
		// Boat
		GameObject randomPrefab = boatSettings.BoatPrefabs[Random.Range(0, boatSettings.BoatPrefabs.Count)];
		if (randomPrefab == null) DebugUtil.ThrowError("RandomPrefab is null. The boatspawner probably doesn't have any boat prefabs assigned.");
		float spawnRadius = boatSettings.sailingRange / 2;
		Vector3 randomPos = new Vector3(orgin.position.x + Random.Range(-spawnRadius, spawnRadius), 0, orgin.position.z + Random.Range(-spawnRadius, spawnRadius));
		
		GameObject = GameObject.Instantiate(randomPrefab, randomPos, Quaternion.identity);
		agent = GameObject.GetComponent<NavMeshAgent>();
		if (agent == null) DebugUtil.ThrowError("Can't find NavMeshAgent on boat");
		agent.speed = Random.Range(boatSettings.SpeedBounds.x, boatSettings.SpeedBounds.y);
		
		// Humans
		int humanAmount = Random.Range(humansSettings.HumanBounds.x, humansSettings.HumanBounds.y);
		for (int i = 0; i < humanAmount; i++)
		{
			Human instance = new Human(GameObject, humansSettings);
			humanCollection.Add(instance);
		}
	}

	public void FixedUpdate()
	{
		humanCollection.FixedUpdate();
		
		if(agent.hasPath) return;
		if (sailPoints.Count == 0) agent.SetDestination(NavMeshUtil.GetRandomPositionOnNavMesh(orgin.position, boatSettings.sailingRange));
		else
		{
		 	agent.SetDestination(sailPoints[sailPointIndex].position);
			sailPointIndex++;
		
			if (sailPointIndex == sailPoints.Count - 1) sailPointIndex = 0;	
		}
	}
	
	public void ChangeID(uint newID)
	{
		ID = newID;
	}
}
