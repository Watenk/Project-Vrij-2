using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : ICharacterController
{
	float rotationX;
	float rotationY;
	
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
	}
	
	public void UpdateRotation(Vector2 rotationInput)
	{
		// Moddel Rotation
		Vector3 direction = rb.velocity.normalized;
		Quaternion targetRotation = Quaternion.LookRotation(direction);
		moddelRoot.transform.rotation = Quaternion.Slerp(moddelRoot.transform.rotation, targetRotation, Time.deltaTime * 10f);
		
		// Camera Rotation
		Debug.Log(rotationInput);
		rotationX += rotationInput.x * characterControllerSettings.RotationSensitivity;
		rotationY += rotationInput.y * characterControllerSettings.RotationSensitivity;
		rotationY = Mathf.Clamp(rotationY, -90, 90);
		cameraRoot.localRotation = Quaternion.Euler(-rotationY, rotationX, 0);
		
		// Camera Tilt
		float targetTilt = Mathf.Repeat(targetRotation.eulerAngles.x + 180f, 360f) - 180f;
		targetTilt = Mathf.Clamp(targetTilt, -50, 50);
		float currentTilt = cinemachineRecomposer.m_Tilt;
		float newTilt = Mathf.Lerp(currentTilt, targetTilt * characterControllerSettings.CameraTiltIntencity, Time.deltaTime);
		cinemachineRecomposer.m_Tilt = newTilt;
	}

	public void UpdateMovement(Vector2 moveInput, float verticalMoveInput)
	{
		Vector3 forward = cameraRoot.forward;
		Vector3 right = cameraRoot.right;
		forward.y = 0f;
		right.y = 0f;
		forward.Normalize();
		right.Normalize();
		Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;
		moveDirection.y = verticalMoveInput;
		moveDirection.Normalize();
		
		rb.AddForce(moveDirection * characterControllerSettings.Speed * Time.deltaTime, ForceMode.Impulse);
	}
}