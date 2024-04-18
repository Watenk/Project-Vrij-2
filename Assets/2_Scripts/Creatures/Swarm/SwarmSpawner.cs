using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmSpawner : MonoBehaviour
{
	public List<CreatureData> creatures = new List<CreatureData>();
	public float WanderRadius = 10f;
	
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, WanderRadius);
	}
}
