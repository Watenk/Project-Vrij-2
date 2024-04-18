using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Controller2 : MonoBehaviour
{
    Rigidbody rb;
    Transform t;

    float startTime;

    [Header("Player Movement")]
    public float Speed;

    float moveX;
    float moveY;
    float moveZ;

    [Header("Player Rotation")]
    public float sensitivity = 1;

    public float rotationMin;
    public float rotationMax;

    float rotationX;
    float rotationY;

    [Header("Animation")]
    float smooth = 5.0f;
    float tiltAngle = 60.0f;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = false;
        RenderSettings.fogColor = new Color(0.2f, 0.4f, 0.8f, 0.5f);
        RenderSettings.fogDensity = 0.04f;

        startTime = Time.time;

        rb = GetComponent<Rigidbody>();
        t = this.transform;

    }

    bool IsUnderwater()
    {
        return gameObject.transform.position.y < 0;
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
        SmoothAfterMovement();
        RenderSettings.fog = IsUnderwater();
        
        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (IsUnderwater())
        {
            Speed = 2;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector3(rb.velocity.x, 3, rb.velocity.z);

            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                rb.velocity = new Vector3(rb.velocity.x, -3, rb.velocity.z);

            }
        }
        else
        {
            Speed = 12;
        }
    }

    void SmoothAfterMovement()
    {
        // Smoothly tilts a transform towards a target rotation.
        float tiltAroundZ = Input.GetAxis("Forward") * tiltAngle;
        float tiltAroundX = Input.GetAxis("Horizontal") * tiltAngle;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        Debug.Log(tiltAngle);

        // smoothing the speed
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) && Speed == 2)
        {
            //maxForwardSpeed = Mathf.SmoothStep(maxForwardSpeed, maxForwardSpeed / 2, 2);
            float t = (Time.time - startTime) / smooth;
            transform.position = new Vector3(Mathf.SmoothStep(Speed, Speed, t), 1, 1);
        }
    }

    void LookAround()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;

        rotationY = Mathf.Clamp(rotationY, rotationMin, rotationMax);

        t.localRotation = Quaternion.Euler(-rotationY, rotationX, 0);
    }

    private void Move()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Forward");

        t.Translate(new Vector3(moveX, 0, moveZ) * Time.deltaTime * Speed);
        t.Translate(new Vector3(0, moveY, 0) * Time.deltaTime * Speed, Space.World);
    }
}
