using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

[CreateAssetMenu(fileName = "SwarmBoidFactory", menuName = "Swarm/BoidFactory")]
public class BoidFactory : ScriptableObject, IFactory<Boid>
{
	/// <summary> Creates a boidInstance </summary>
	/// <param name="Prefabs"> Gameobject[] </param>
	/// <param name="SpawnPos"> Vector3 </param>
	/// <param name="Parent"> Transform </param>
	/// <returns> Boid </returns>
	public Boid Construct(params object[] parameters)
	{
		GameObject[] prefabs = (GameObject[])parameters[0];
		Vector3 spawnPos = (Vector3)parameters[1];
		Transform parent = (Transform)parameters[2];
		
		GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Length)];
		GameObject instance = GameObject.Instantiate(randomPrefab, spawnPos, Quaternion.identity, parent);
			
		Boid boid = instance.GetComponent<Boid>();
		DebugUtil.ThrowError(instance.name + "doesn't contain a Boid");
		
		return boid;
	}
}
