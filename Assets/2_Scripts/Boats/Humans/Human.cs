using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Watenk;

public class Human : IGameObject, IID
{
	public uint ID { get; private set; }
	public GameObject GameObject { get; private set; }
	
	private Fsm<Human> behaviourFSM;
	
	// Dependencies
	public HumansSettings humansSettings { get; private set; }
	public SirenLocation sirenLocation { get; private set; }
	public GameObject platform { get; private set; }
	public GameObject parent { get; private set; }
	public DictCollection<Human> humans { get; private set; }

	public Human(DictCollection<Human> humans, GameObject parent, GameObject platform, HumansSettings humansSettings, SirenLocation sirenLocation)
	{
		this.humans = humans;
		this.parent = parent;
		this.platform = platform;
		this.humansSettings = humansSettings;
		this.sirenLocation = sirenLocation;
		
		behaviourFSM = new Fsm<Human>(this, 
			new HumanIdleState(),
			new HumanAttackState(),
			new HumanWanderState()
		);
		
		behaviourFSM.States.TryGetValue(typeof(HumanIdleState), out BaseState<Human> idleState);
		((HumanIdleState)idleState).IdleTimer.OnTimer += OnIdleTimer;
		
		GameObject randomPrefab = humansSettings.HumanPrefabs[Random.Range(0, humansSettings.HumanPrefabs.Count)];
		if (randomPrefab == null) DebugUtil.ThrowError("RandomPrefab is null. The boatspawner probably doesn't have any boat prefabs assigned.");
		
		GameObject = GameObject.Instantiate(randomPrefab, GenerateRandomHumanPos(), Quaternion.identity, parent.transform);
	}
	
	~Human()
	{
		behaviourFSM.States.TryGetValue(typeof(HumanIdleState), out BaseState<Human> idleState);
		((HumanIdleState)idleState).IdleTimer.OnTimer -= OnIdleTimer;
	}

	public void ChangeID(uint newID)
	{
		ID = newID;
	}

	public void FixedUpdate(DictCollection<Human> humans)
	{
		this.humans = humans;
		
		if (Vector3.Distance(sirenLocation.Position, GameObject.transform.position) <= humansSettings.SirenDetectRange)
		{
			if (behaviourFSM.CurrentState.GetType() != typeof(HumanAttackState))
			{
				behaviourFSM.SwitchState(typeof(HumanAttackState));
			}
		}
		else if (behaviourFSM.CurrentState.GetType() == typeof(HumanAttackState))
		{
			behaviourFSM.SwitchState(typeof(HumanIdleState));
		}
		
		behaviourFSM.Update();
	}
	
	public Vector3 GenerateRandomHumanPos()
	{
		Vector3 randomPos = Vector3.zero;
		bool getting = true;
		int maxTries = 1000;
		int tries = 0;
		while (getting)
		{
			randomPos = GenerateRandomPlatformPos();
			if (CheckIfOccupied(randomPos)) getting = false;
			tries++;
			if (tries >= maxTries)
			{
				DebugUtil.ThrowWarning("Couldn't get humanPos. This is probably because the seperation distance is too high or the human amount is too high to fit on the boat.");
				break;
			}
		}
		
		return randomPos;
	}
	
	private bool CheckIfOccupied(Vector3 newPos)
	{
		foreach (var kvp in humans.Collection)
		{
			if (Vector3.Distance(newPos, kvp.Value.GameObject.transform.position) < humansSettings.SeperationDistance)
			{
				return false;
			}
		}
		return true;
	}
	
	private Vector3 GenerateRandomPlatformPos()
	{
		Vector3 randomPos = new Vector3
		{
			x = Random.Range(platform.transform.position.x - (platform.transform.localScale.x / 2) + (humansSettings.HumanPrefabs[0].transform.localScale.x / 2),
							 platform.transform.position.x + (platform.transform.localScale.x / 2) - (humansSettings.HumanPrefabs[0].transform.localScale.x / 2)),
			y = platform.transform.position.y + (humansSettings.HumanPrefabs[0].transform.localScale.y / 2),
			z = Random.Range(platform.transform.position.z - (platform.transform.localScale.z / 2) + (humansSettings.HumanPrefabs[0].transform.localScale.z / 2),
							 platform.transform.position.z + (platform.transform.localScale.z / 2) - (humansSettings.HumanPrefabs[0].transform.localScale.z / 2))
		};
		return randomPos;
	}
	
	private void OnIdleTimer()
	{
		behaviourFSM.SwitchState(typeof(HumanWanderState));
	}
}
