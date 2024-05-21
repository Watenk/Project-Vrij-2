using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watenk;

[CreateAssetMenu(fileName = "DefaultBoidFactory", menuName = "Swarm/DefaultBoidFactory")]
public class BoidFactory : ScriptableObject, IFactory<Boid>
{
	public Boid Construct(params object[] parameters)
	{
		BoidSettings boidSettings = (BoidSettings)parameters[0];
		Vector3 spawnPos = (Vector3)parameters[1];
		Transform parent = (Transform)parameters[2];
		
		GameObject randomPrefab = boidSettings.Prefabs[Random.Range(0, boidSettings.Prefabs.Length)];
		GameObject instance = GameObject.Instantiate(randomPrefab, spawnPos, Quaternion.identity, parent);
			
		return new Boid(boidSettings, instance);
	}

	public void Deconstruct(Boid boid)
	{
		GameObject.Destroy(boid.GameObject);
	}
}