using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatsSpawner : MonoBehaviour
{
	private BoatCollection boatCollection;
	
	[Header("Settings")]
	[SerializeField]
	private byte boatAmount;
	[SerializeField] [Tooltip("The range the boat will be able to sail in")] 
	private byte sailingRange;
	[SerializeField]
	private BoatSettings boatSettings;
	
	public void Start() 
	{
		boatCollection = new BoatCollection();
		
		for (int i = 0; i < boatAmount; i++)
		{
			boatCollection.Add(boatSettings);
		}
	}
	
	#if UNITY_EDITOR
	public void OnDrawGizmosSelected()
	{
		// SailRange Sphere
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(gameObject.transform.position, sailingRange);
	}
	#endif
}
