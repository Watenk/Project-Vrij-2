using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Watenk;

public class Boat : IGameObject, IFixedUpdateable
{
	public uint ID { get; private set;}
	public GameObject GameObject { get; private set; }

	private float speed;
	private int sailPointIndex;
	private DictCollectionFixedUpdate<Human> humanCollection = new DictCollectionFixedUpdate<Human>();
	
	// Dependencies
	private List<Transform> sailPoints;
	private BoatsSettings boatsSettings;
	private NavMeshAgent agent;
	private Transform orginPos;
	
	public Boat(BoatsSettings boatsSettings, HumansSettings humansSettings, Transform orginPos, List<Transform> sailPoints)
	{
		this.boatsSettings = boatsSettings;
		this.sailPoints = sailPoints;
		this.orginPos = orginPos;
		sailPointIndex = Random.Range(0, sailPoints.Count - 1);
		
		// Boat
		GameObject randomPrefab = boatsSettings.BoatPrefabs[Random.Range(0, boatsSettings.BoatPrefabs.Count)];
		if (randomPrefab == null) DebugUtil.ThrowError("RandomPrefab is null. The boatspawner probably doesn't have any boat prefabs assigned.");
		float spawnRadius = boatsSettings.sailingRange / 2;
		Vector3 randomPos = new Vector3(orginPos.position.x + Random.Range(-spawnRadius, spawnRadius), 0, orginPos.position.z + Random.Range(-spawnRadius, spawnRadius));
		
		GameObject = GameObject.Instantiate(randomPrefab, randomPos, Quaternion.identity);
		agent = GameObject.GetComponent<NavMeshAgent>();
		if (agent == null) DebugUtil.ThrowError("Can't find NavMeshAgent on boat");
		agent.speed = Random.Range(boatsSettings.SpeedBounds.x, boatsSettings.SpeedBounds.y);
		
		// Humans
		int humanAmount = Random.Range(humansSettings.HumanBounds.x, humansSettings.HumanBounds.y + 1);
		List<Vector3> occupiedPos = new List<Vector3>();
		for (int i = 0; i < humanAmount; i++)
		{
			Vector3 randomPosOnBoat = GetRandomHumanPos(humansSettings, occupiedPos, humansSettings.SperationDistance);
			occupiedPos.Add(randomPosOnBoat);
			
			Human human = new Human(GameObject, humansSettings, randomPosOnBoat);
			humanCollection.Add(human);
		}
	}

	public void FixedUpdate()
	{
		if(agent.hasPath) return;
		if (sailPoints.Count == 0) agent.SetDestination(NavMeshUtil.GetRandomPositionOnNavMesh(orginPos.position, boatsSettings.sailingRange));
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
	
	private Vector3 GetRandomHumanPos(HumansSettings humanSettings, List<Vector3> occupiedPos, float seperationDistance)
	{
		bool getting = true;
		Vector3 randomPos = Vector3.zero;
		int maxTries = 1000;
		int tries = 0;
		while (getting)
		{
			randomPos = GenerateRandomPos(humanSettings);
			if (CheckIfOccupied(randomPos, occupiedPos, seperationDistance)) getting = false;
			tries++;
			if (tries >= maxTries)
			{
				DebugUtil.ThrowWarning("Couldn't instance all humans. This is probably because the seperation distance is too high or the human amount is too high to fit on the boat.");
				break;
			}
		}
		
		return randomPos;
	}
	
	private bool CheckIfOccupied(Vector3 newPos, List<Vector3> occupiedPos, float seperationDistance)
	{
		foreach (Vector3 currentOccupiedPos in occupiedPos)
		{
			if (Vector3.Distance(newPos, currentOccupiedPos) < seperationDistance)
			{
				return false;
			}
		}
		return true;
	}
	
	private Vector3 GenerateRandomPos(HumansSettings humanSettings)
	{
		Vector3 randomPos = new Vector3();
		randomPos.x = Random.Range(GameObject.transform.position.x - (GameObject.transform.GetChild(0).transform.localScale.x / 2) + (humanSettings.HumanPrefabs[0].transform.localScale.x / 2), 
								   GameObject.transform.position.x + (GameObject.transform.GetChild(0).transform.localScale.x / 2) - (humanSettings.HumanPrefabs[0].transform.localScale.x / 2));
		randomPos.y = GameObject.transform.position.y + humanSettings.HumanPrefabs[0].transform.localScale.y / 2;
		randomPos.z = Random.Range(GameObject.transform.position.z - (GameObject.transform.GetChild(0).transform.localScale.z / 2) + (humanSettings.HumanPrefabs[0].transform.localScale.z / 2), 
								   GameObject.transform.position.z + (GameObject.transform.GetChild(0).transform.localScale.z / 2) - (humanSettings.HumanPrefabs[0].transform.localScale.z / 2));
		return randomPos;
	}
}
