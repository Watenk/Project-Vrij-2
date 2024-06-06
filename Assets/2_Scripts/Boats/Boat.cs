using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Watenk;

public class Boat : IGameObject, IFixedUpdateable, IID, IHealth<Boat>
{
	public event IHealth<Boat>.HealthChangeEventHandler OnHealthChanged;
	public event IHealth<Boat>.DeathEventHandler OnDeath;
	
	public uint ID { get; private set;}
	public GameObject GameObject { get; private set; }
	public int HP { get; private set; }
	public int MaxHP { get; private set; }

	private int sailPointIndex;
	private Timer destinationTimer = new Timer(1);
	private DictCollection<Human> humanCollection = new DictCollection<Human>();
	
	// Dependencies
	private List<Transform> sailPoints;
	private BoatsSettings boatsSettings;
	private NavMeshAgent agent;
	private Transform orginPos;

	public Boat(BoatsSettings boatsSettings, HumansSettings humansSettings, Transform orginPos, List<Transform> sailPoints, SirenLocation sirenLocation)
	{
		this.boatsSettings = boatsSettings;
		this.sailPoints = sailPoints;
		this.orginPos = orginPos;
		sailPointIndex = Random.Range(0, sailPoints.Count);
		
		MaxHP = 1;
		HP = MaxHP;
		
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
		for (int i = 0; i < humanAmount; i++)
		{
			Human human = new Human(humanCollection, GameObject, GameObject.transform.GetChild(0).gameObject, humansSettings, sirenLocation);
			human.OnDeath += OnDeadHuman;
			humanCollection.Add(human);
		}
	}

	public void FixedUpdate()
	{
		foreach (var kvp in humanCollection.Collection)
		{
			kvp.Value.FixedUpdate(humanCollection);
		}
		destinationTimer.Tick(Time.deltaTime);
		
		if(agent.remainingDistance >= 3 || destinationTimer.TimeLeft > 0) return;
		if (sailPoints.Count == 0) agent.SetDestination(NavMeshUtil.GetRandomPositionOnNavMesh(orginPos.position, boatsSettings.sailingRange));
		else
		{
		 	agent.SetDestination(sailPoints[sailPointIndex].position);
			sailPointIndex++;
			destinationTimer.Reset();
		
			if (sailPointIndex == sailPoints.Count) sailPointIndex = 0;	
		}
	}
	
	public void ChangeID(uint newID)
	{
		ID = newID;
	}
	
	public void OnDeadHuman(Human human)
	{
		human.OnDeath -= OnDeadHuman;
		humanCollection.Remove(human);
		GameObject.Destroy(human.GameObject);
		if (humanCollection.GetCount() == 0)
		{
			ChangeHealth(-1);
		}
	}

	public void ChangeHealth(int amount)
	{
		HP += amount;
		OnHealthChanged?.Invoke(this);
		
		if (HP <= 0)
		{
			Die();
		}
	}

	public void Die()
	{
		OnDeath?.Invoke(this);
	}
}
