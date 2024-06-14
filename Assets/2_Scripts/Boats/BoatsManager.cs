using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatsManager : MonoBehaviour
{
	public static event Action<int> OnBoatSunk = delegate { };

	private DictCollection<Boat> boatCollection = new DictCollection<Boat>();
	private List<Boat> sunkenBoats = new List<Boat>();
	
	[Header("Settings")]
	[SerializeField]
	private byte boatAmount;
	[SerializeField] [Tooltip("The points the boats will sail to")] 
	private List<Transform> SailPoints = new List<Transform>();
	[SerializeField]
	private BoatsSettings boatSettings;
	[SerializeField]
	private HumansSettings humansSettings;
	[SerializeField]
	private SirenLocation sirenLocation;
	
	public void Start() 
	{
		for (int i = 0; i < boatAmount; i++)
		{
			Boat instance = new Boat(boatSettings, humansSettings, this.transform, SailPoints, sirenLocation);	
			instance.OnDeath += OnBoatDead;
			boatCollection.Add(instance);
		}
	}
	
	public void FixedUpdate() 
	{
		foreach (var kvp in boatCollection.Collection)
		{
			kvp.Value.FixedUpdate();
		}
		
		foreach (Boat boat in sunkenBoats)
		{
			boatCollection.Remove(boat);
			GameObject.Destroy(boat.GameObject);
			ServiceLocator.Instance.Get<EventManager>().Invoke(Event.OnBoatSunk);
		}
		//sunkenBoats.Clear();
	}
	
	#if UNITY_EDITOR
	public void OnDrawGizmosSelected()
	{
		// SailRange Sphere
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(gameObject.transform.position, boatSettings.sailingRange);
	}
	#endif
	
	private void OnBoatDead(Boat boat)
	{
		sunkenBoats.Add(boat);
		OnBoatSunk(sunkenBoats.Count);
	}
}
