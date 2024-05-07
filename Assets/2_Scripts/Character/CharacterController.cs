using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Watenk;

public class CharacterController : IUpdateable
{
	private float rotationX;
	private float rotationY;
	
	// Inputs
	private PlayerInputs inputs;
	private InputAction move;
	private InputAction verticalMove;
	private InputAction look;
	
	// Dependencies
	private CharacterControllerSettings characterControllerSettings;
	private Rigidbody rb;
	private Transform cameraRoot;
	private Transform moddelRoot;
	private CinemachineRecomposer cinemachineRecomposer;
	
	public CharacterController(CharacterControllerSettings characterControllerSettings, Rigidbody rb, Transform cameraRoot, Transform moddelRoot, CinemachineRecomposer cinemachineRecomposer)
	{
		this.characterControllerSettings = characterControllerSettings;
		this.rb = rb;
		this.cameraRoot = cameraRoot;
		this.moddelRoot = moddelRoot;
		this.cinemachineRecomposer = cinemachineRecomposer;

		EnableInputs();
	}
	
	~CharacterController()
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
		moddelRoot.transform.rotation = Quaternion.Slerp(moddelRoot.transform.rotation, targetRotation, Time.deltaTime * 10f);
		
		// Camera Rotation
		rotationX += Input.GetAxis("Mouse X");
		rotationY += Input.GetAxis("Mouse Y");
		//rotationY = Mathf.Clamp(rotationY, RotationMin, RotationMax);
		cameraRoot.localRotation = Quaternion.Euler(-rotationY, rotationX, 0);
		
		// Camera Tilt
		float targetTilt = Mathf.Repeat(targetRotation.eulerAngles.x + 180f, 360f) - 180f;
		targetTilt = Mathf.Clamp(targetTilt, -50, 50);
		float currentTilt = cinemachineRecomposer.m_Tilt;
		float newTilt = Mathf.Lerp(currentTilt, targetTilt * characterControllerSettings.CameraTiltIntencity, Time.deltaTime);
		cinemachineRecomposer.m_Tilt = newTilt;
	}

	private void Movement()
	{
		// Calc MoveDirection
		Vector2 moveInput = move.ReadValue<Vector2>();
		Vector3 forward = cameraRoot.forward;
		Vector3 right = cameraRoot.right;
		forward.y = 0f;
		right.y = 0f;
		forward.Normalize();
		right.Normalize();
		Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;
		moveDirection.y = verticalMove.ReadValue<float>();
		moveDirection.Normalize();
		
		rb.AddForce(moveDirection * characterControllerSettings.Speed * Time.deltaTime, ForceMode.Impulse);
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