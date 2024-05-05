using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : IGameObject, IDamageable, IUpdateable
{
	public int Health { get; private set; }
	public GameObject GameObject { get; private set; }
	
	private PLayerMovement playerMovement;

	public Player(GameObject gameObject, Transform cameraRoot, Transform attackRoot, GameObject sirenModdel, CinemachineRecomposer cinemachineRecomposer, float speed, float attackRange, float cameraTiltIntencity)
	{
		GameObject = gameObject;
		
		this.playerMovement = new PLayerMovement(this, cameraRoot, attackRoot, sirenModdel, cinemachineRecomposer, speed, attackRange, cameraTiltIntencity);
	}

	public void Update()
	{
		playerMovement.Update();
	}
	
	public void TakeDamage(int amount)
	{
		throw new System.NotImplementedException();
	}
}
