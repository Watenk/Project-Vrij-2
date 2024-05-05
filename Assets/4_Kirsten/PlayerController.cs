using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PLayerController : MonoBehaviour
{
	public Transform CameraRotation;
	public GameObject SirenModdel;
	public CinemachineRecomposer cinemachineRecomposer;
	public bool EatedFish = false;
	public float CameraTiltIntencity;
	
	private Rigidbody rb;
	private float startTime;


	[Header("Player Movement")]
	public float Speed;
	public float AttackRange;

	[Header("Player Rotation")]
	public float Sensitivity = 1;

	public float RotationMin;
	public float RotationMax;

	private float rotationX;
	private float rotationY;

	[Header("Animation")]
	public float Smooth = 5.0f;
	public float TiltAngle = 60.0f;
	
	private PlayerInputs inputs;
	private InputAction move;
	private InputAction verticalMove;
	private InputAction look;
	
	private void OnEnable()
	{
		EnableInputs();
	}
	
	private void OnDisable() {
		move.Disable();
		verticalMove.Disable();
		look.Disable();
	}

	private void Start()
	{
		//RenderSettings.fog = false;
		//RenderSettings.fogColor = new Color(0.2f, 0.4f, 0.8f, 0.5f);
		//RenderSettings.fogDensity = 0.04f;

		EnableInputs();

		startTime = Time.time;
		//rb.useGravity = false;

		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		LookAround();
		Move();
		Attack();
		//SmoothAfterMovement();
		
		if (Input.GetKey(KeyCode.Escape))
		{
			Cursor.lockState = CursorLockMode.None;
		}

		if (IsUnderwater())
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				rb.velocity = new Vector3(rb.velocity.x, 3, rb.velocity.z);

			}

			if (Input.GetKeyDown(KeyCode.LeftControl))
			{
				rb.velocity = new Vector3(rb.velocity.x, -3, rb.velocity.z);

			}
		}
	}

	#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, AttackRange);
	}
	#endif

	private void EnableInputs()
	{
		if (inputs == null) inputs = new PlayerInputs();
		
		move = inputs.Player.Move;
		move.Enable();
		
		verticalMove = inputs.Player.VerticalMove;
		verticalMove.Enable();
		
		look = inputs.Player.Look;
		look.Enable();
	}

	private bool IsUnderwater()
	{
		return gameObject.transform.position.y < 0;
	}

	private void SmoothAfterMovement()
	{
		// Smoothly tilts a transform towards a target rotation.
		float tiltAroundZ = Input.GetAxis("Forward") * TiltAngle;
		float tiltAroundX = Input.GetAxis("Horizontal") * TiltAngle;

		// Rotate the cube by converting the angles into a quaternion.
		Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

		// Dampen towards the target rotation
		SirenModdel.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * Smooth);

		// smoothing the speed
		//if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) && Speed == 2)
		//{
		//    //maxForwardSpeed = Mathf.SmoothStep(maxForwardSpeed, maxForwardSpeed / 2, 2);
		//    float t = (Time.time - startTime) / smooth;
		//    transform.position = new Vector3(Mathf.SmoothStep(Speed, Speed, t), 1, 1);
		//}
	}

	private void Attack()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, AttackRange);
		foreach (var hitCollider in hitColliders)
		{
			if (hitCollider.CompareTag("Fish"))
			{
				Debug.Log("Ik zie fish");

				if (Input.GetMouseButtonDown(1))
				{
					Destroy(hitCollider.gameObject);
					EatedFish = true;

				}
			}
		}
	}

	private void LookAround()
	{
		rotationX += Input.GetAxis("Mouse X") * Sensitivity;
		rotationY += Input.GetAxis("Mouse Y") * Sensitivity;

		rotationY = Mathf.Clamp(rotationY, RotationMin, RotationMax);

		CameraRotation.localRotation = Quaternion.Euler(-rotationY, rotationX, 0);
		
		// Rotation
		Vector3 direction = rb.velocity.normalized;
		Quaternion targetRotation = Quaternion.LookRotation(direction);
		SirenModdel.transform.rotation = Quaternion.Slerp(SirenModdel.transform.rotation, targetRotation, Time.deltaTime * 10f);
		
		float targetTilt = Mathf.Repeat(targetRotation.eulerAngles.x + 180f, 360f) - 180f;

		targetTilt = Mathf.Clamp(targetTilt, -50, 50);

		float currentTilt = cinemachineRecomposer.m_Tilt;
		float newTilt = Mathf.Lerp(currentTilt, targetTilt * CameraTiltIntencity, Time.deltaTime);
		cinemachineRecomposer.m_Tilt = newTilt;
	}

	private void Move()
	{
		Vector2 moveInput = move.ReadValue<Vector2>();

		Vector3 forward = CameraRotation.forward;
		Vector3 right = CameraRotation.right;

		forward.y = 0f;
		right.y = 0f;
		forward.Normalize();
		right.Normalize();

		Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;
		moveDirection.y = verticalMove.ReadValue<float>();
		moveDirection.Normalize();
		
		rb.AddForce(moveDirection * Speed * Time.deltaTime, ForceMode.Impulse);
	}
}