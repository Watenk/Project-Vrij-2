using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatsManager : MonoBehaviour
{
	private DictCollectionFixedUpdate<Boat> boatCollection;
	
	[Header("Settings")]
	[SerializeField]
	private byte boatAmount;
	[SerializeField] [Tooltip("The points the boats will sail to")] 
	private List<Transform> SailPoints = new List<Transform>();
	[SerializeField]
	private BoatsSettings boatSettings;
	[SerializeField]
	private HumansSettings humansSettings;
	public void Start() 
	{
		boatCollection = new DictCollectionFixedUpdate<Boat>();
		for (int i = 0; i < boatAmount; i++)
		{
			Boat instance = new Boat(boatSettings, humansSettings, this.transform, SailPoints);	
			boatCollection.Add(instance);
		}
	}
	
	public void FixedUpdate() 
	{
		boatCollection.FixedUpdate();
	}
	
	#if UNITY_EDITOR
	public void OnDrawGizmosSelected()
	{
		// SailRange Sphere
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(gameObject.transform.position, boatSettings.sailingRange);
	}
	#endif
}
