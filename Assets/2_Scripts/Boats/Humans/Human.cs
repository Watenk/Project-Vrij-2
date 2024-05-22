using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Watenk;

public class Human : IGameObject, IFixedUpdateable, IID
{
	public uint ID { get; private set; }
	public GameObject GameObject { get; private set; }
	
	private bool attacking;
	private float attackDelay;
	
	// Dependencies
	private HumansSettings humansSettings;
	private SirenLocation sirenLocation;
	private GameObject parent;

	public Human(GameObject boat, HumansSettings humansSettings, Vector3 spawnPos, SirenLocation sirenLocation)
	{
		this.parent = boat;
		this.humansSettings = humansSettings;
		this.sirenLocation = sirenLocation;
		attackDelay = humansSettings.AttackDelay;
		
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
		if (Vector3.Distance(sirenLocation.Position, GameObject.transform.position) <= humansSettings.SirenDetectRange)
		{
			attacking = true;
		}
		else
		{
			attacking = false;
		}
		
		if (attacking)
		{
			Quaternion targetRotation = Quaternion.LookRotation(sirenLocation.Position - GameObject.transform.position);
			GameObject.transform.rotation = Quaternion.Lerp(GameObject.transform.rotation, targetRotation, Time.deltaTime * humansSettings.RotationSpeed);
			attackDelay -= Time.deltaTime;
			
			if (attackDelay <= 0)
			{
				Attack();
				attackDelay = humansSettings.AttackDelay;
			}
		}
	}
	
	private void Attack()
	{
		int weaponAmount = humansSettings.ThrowingWeaponsPrefabs.Count;
		GameObject randomWeaponPrefab = humansSettings.ThrowingWeaponsPrefabs[Random.Range(0, weaponAmount)];
		GameObject weaponInstance = GameObject.Instantiate(randomWeaponPrefab, GameObject.transform.position, Quaternion.identity);
		weaponInstance.transform.rotation = Quaternion.LookRotation(sirenLocation.Position - GameObject.transform.position);
		Rigidbody weaponRigidbody = weaponInstance.GetComponent<Rigidbody>();
		weaponRigidbody.AddForce(weaponInstance.transform.forward * humansSettings.WeaponThrowSpeed);
	}
}
