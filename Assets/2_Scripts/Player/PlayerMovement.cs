using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Watenk;

public class PLayerMovement : IUpdateable
{
	// References
	public Transform CameraRoot;
	public Transform AttackRoot;
	public GameObject SirenModdel;
	public CinemachineRecomposer CinemachineRecomposer;
	
	// Movement
	public float Speed;
	public float AttackRange;
	public float CameraTiltIntencity;
	
	private PlayerInputs inputs;
	private InputAction move;
	private InputAction verticalMove;
	private InputAction look;
	private float rotationX;
	private float rotationY;
	
	// Dependencies
	private Player player;
	private Rigidbody rb;
	
	public PLayerMovement(Player player, Transform cameraRoot, Transform attackRoot, GameObject sirenModdel, CinemachineRecomposer cinemachineRecomposer, float speed, float attackRange, float cameraTiltIntencity)
	{
		this.player = player;
		CameraRoot = cameraRoot;
		AttackRoot = attackRoot;
		SirenModdel = sirenModdel;
		CinemachineRecomposer = cinemachineRecomposer;
		Speed = speed;
		AttackRange = attackRange;
		CameraTiltIntencity = cameraTiltIntencity;
		
		rb = player.GameObject.GetComponent<Rigidbody>();
		if (rb == null)DebugUtil.TrowError("Can't find RigidBody on " + player.GameObject.name);
		
		EnableInputs();
	}
	
	~PLayerMovement()
	{
		DisableInputs();
	}
	
	public void Update()
	{
		Rotation();
		Movement();
	}

	private void Rotation()
	{
		// Moddel Rotation
		Vector3 direction = rb.velocity.normalized;
		Quaternion targetRotation = Quaternion.LookRotation(direction);
		SirenModdel.transform.rotation = Quaternion.Slerp(SirenModdel.transform.rotation, targetRotation, Time.deltaTime * 10f);
		
		// Camera Rotation
		rotationX += Input.GetAxis("Mouse X");
		rotationY += Input.GetAxis("Mouse Y");
		//rotationY = Mathf.Clamp(rotationY, RotationMin, RotationMax);
		CameraRoot.localRotation = Quaternion.Euler(-rotationY, rotationX, 0);
		
		// Camera Tilt
		float targetTilt = Mathf.Repeat(targetRotation.eulerAngles.x + 180f, 360f) - 180f;
		targetTilt = Mathf.Clamp(targetTilt, -50, 50);
		float currentTilt = CinemachineRecomposer.m_Tilt;
		float newTilt = Mathf.Lerp(currentTilt, targetTilt * CameraTiltIntencity, Time.deltaTime);
		CinemachineRecomposer.m_Tilt = newTilt;
	}

	private void Movement()
	{
		// Calc MoveDirection
		Vector2 moveInput = move.ReadValue<Vector2>();
		Vector3 forward = CameraRoot.forward;
		Vector3 right = CameraRoot.right;
		forward.y = 0f;
		right.y = 0f;
		forward.Normalize();
		right.Normalize();
		Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;
		moveDirection.y = verticalMove.ReadValue<float>();
		moveDirection.Normalize();
		
		rb.AddForce(moveDirection * Speed * Time.deltaTime, ForceMode.Impulse);
	}
	
	private void EnableInputs()
	{
		if (inputs == null)
		{
			inputs = new PlayerInputs();
			move = inputs.Player.Move;	
			verticalMove = inputs.Player.VerticalMove;
			look = inputs.Player.Look;
		}
		
		move.Enable();
		verticalMove.Enable();
		look.Enable();
	}
	
	private void DisableInputs()
	{
		move.Disable();
		verticalMove.Disable();
		look.Disable();
	}
}


	// Needs its own class
	
	// private void Attack()
	// {
	// 	Collider[] hitColliders = Physics.OverlapSphere(player.GameObject.transform.position, AttackRange);
	// 	foreach (var hitCollider in hitColliders)
	// 	{
	// 		if (hitCollider.CompareTag("Fish"))
	// 		{
	// 			Debug.Log("Ik zie fish");

	// 			if (Input.GetMouseButtonDown(1))
	// 			{
	// 				GameObject.Destroy(hitCollider.gameObject);
	// 				EatedFish = true;

	// 			}
	// 		}
	// 	}
	// }