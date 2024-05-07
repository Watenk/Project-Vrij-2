using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
	[Header("References")]
	[Tooltip("The point the camera will rotate around")]
	public Transform CameraRoot;
	[Tooltip("The point the player will attack from")]
	public Transform AttackRoot;
	public GameObject SirenModdel;
	public CinemachineRecomposer CinemachineRecomposer;

	[Header("Movement")]
	public float Speed;
	public float AttackRange;
	[Tooltip("The amount the camera will tilt by up and down movement")]
	public float CameraTiltIntencity;
	
	private void Start()
	{
		Player player = new Player(this.gameObject, CameraRoot, AttackRoot, SirenModdel, CinemachineRecomposer, Speed, AttackRange, CameraTiltIntencity);
		
		PlayerManager playerManager = ServiceManager.Instance.Get<PlayerManager>();
		playerManager.Add(player);
		
		Destroy(this);
	}
	
	#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(AttackRoot.position, AttackRange);
	}
	#endif
}
