using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwimming : MonoBehaviour
{
    Transform t;

    public LayerMask waterMask;

    public static bool isSwimming;
    public static bool isFloating;

    //Vector3 refVelocity = Vector3.zero; // This is used inside the function, don't touch!
    //float smoothing = 0.5f; // How much smoothing there should be.

    [Header("Player Rotation")]
    public float sensitivity = 1;

    public float rotationMin;
    public float rotationMax;

    float rotationX;
    float rotationY;

    [Header("Player Movement")]
    public float speed = 1;

    float moveX;
    float moveY;
    float moveZ;

    bool stopVerticalMovement;

    Rigidbody rb;

    public Collider waterLine;

    [Header("Animation")]
    float smooth = 5.0f;
    float tiltAngle = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Smoothly tilts a transform towards a target rotation.
        float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        rb = GetComponent<Rigidbody>();
        t = this.transform;

        Cursor.lockState = CursorLockMode.Locked;

        
    }

    private void FixedUpdate()
    {
        SwimmingOrFloating();
        Move();

    }

    private void OnTriggerEnter(Collider waterLine)
    {
        isSwimming = true;
        stopVerticalMovement = true;
        Debug.Log(stopVerticalMovement);

        if (stopVerticalMovement == true)
        {
            moveY = Mathf.Min(moveY, 0);
        }
    }

    private void OnTriggerExit(Collider waterLine)
    {
        stopVerticalMovement = false;
    }

    void SwimmingOrFloating ()
    {

        if (Input.GetKeyUp("w") || Input.GetKeyUp("a") || Input.GetKeyUp("s") || Input.GetKeyUp("d"))
        {
            speed = Mathf.SmoothStep(speed, speed / 2, 2);
        }

    }

    void SwitchMovement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            
            Color newColor = new Color(0.3f, 0.4f, 0.6f, 0.3f);
        }
        else
        {
            Color newColor = new Color(1.2f, 0.9f, 0.6f, 0.6f);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        LookAround();

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void LookAround()
    {
        rotationX += Input.GetAxis("Mouse X")* sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;

        rotationY = Mathf.Clamp(rotationY, rotationMin, rotationMax);

        t.localRotation = Quaternion.Euler(-rotationY, rotationX, 0);
    }

    void Move()
    {
        SwitchMovement();
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        moveZ = Input.GetAxis("Forward");

        if (!isFloating) 
        {
            rb.velocity = new Vector2(0, 0);
        }
        else
        {
            if (moveX == 0 && moveZ == 0)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        if (!isSwimming)
        {
            moveY = Mathf.Min(moveY, 0);

            Vector3 clampedDirection = t.TransformDirection(new Vector3(moveX, moveY, moveZ));

            clampedDirection = new Vector3(clampedDirection.x, Mathf.Min(clampedDirection.y, 0), clampedDirection.z);

            t.Translate(clampedDirection * Time.deltaTime * speed, Space.World);
        }
        else
        {
            t.Translate(new Vector3(moveX, 0, moveZ) * Time.deltaTime * speed);
            t.Translate(new Vector3(0, moveY, 0) * Time.deltaTime * speed, Space.World);

        }

    }
}

   
