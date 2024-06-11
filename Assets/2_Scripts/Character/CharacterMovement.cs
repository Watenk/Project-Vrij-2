using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : ICharacterMovement
{
	float rotationX;
	float rotationY;
	
	// Dependencies
	private CharacterMovementSettings characterControllerSettings;
	private Rigidbody rb;
	private Transform cameraRoot;
	private Transform moddelRoot;
	private CinemachineRecomposer cinemachineRecomposer;
	private Transform waterSurface;
	private Timer boostCooldownTimer;
	
	private bool boost;
	
	public CharacterController(CharacterMovementSettings characterControllerSettings, Rigidbody rb, Transform cameraRoot, Transform moddelRoot, CinemachineRecomposer cinemachineRecomposer, Transform waterSurface)
	{
		this.characterControllerSettings = characterControllerSettings;
		this.rb = rb;
		this.cameraRoot = cameraRoot;
		this.moddelRoot = moddelRoot;
		this.cinemachineRecomposer = cinemachineRecomposer;
		this.waterSurface = waterSurface;
		
		boostCooldownTimer = new Timer(characterControllerSettings.BoostCooldownLenght);
	}
	
	public void UpdateRotation(Vector2 rotationInput)
	{
		// Moddel Rotation
		Vector3 direction = rb.velocity.normalized;
		if (direction == Vector3.zero) return;
		
		Quaternion targetRotation = Quaternion.LookRotation(direction);
		moddelRoot.transform.rotation = Quaternion.Slerp(moddelRoot.transform.rotation, targetRotation, Time.deltaTime * 10f);
		
		// Camera Rotation
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
		boostCooldownTimer.Tick(Time.deltaTime);
		
		Vector3 forward = cameraRoot.forward;
		Vector3 right = cameraRoot.right;
		forward.y = 0f;
		right.y = 0f;
		forward.Normalize();
		right.Normalize();
		Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;
		moveDirection.y = verticalMoveInput;
		moveDirection.Normalize();
		
		if (Underwater())
		{
			rb.AddForce(moveDirection * characterControllerSettings.Speed * Time.deltaTime, ForceMode.Impulse);
		}
		else
		{
			rb.AddForce(rb.gameObject.transform.up * -characterControllerSettings.Gravity * Time.deltaTime, ForceMode.Impulse);
			rb.AddForce(moveDirection * (characterControllerSettings.Speed / 5) * Time.deltaTime, ForceMode.Impulse);
		}
		
		if (boost)
		{
			rb.AddForce(moveDirection * characterControllerSettings.BoostStrenght * Time.deltaTime, ForceMode.Impulse);
			boost = false;
		}
	}
	
	public void Boost()
	{
		if (boostCooldownTimer.TimeLeft <= 0)
		{
			boost = true;
			boostCooldownTimer.Reset();
		}
	}
	
	private bool Underwater()
	{
		if (rb.gameObject.transform.position.y <= waterSurface.position.y) return true;
		return false;
	}
}