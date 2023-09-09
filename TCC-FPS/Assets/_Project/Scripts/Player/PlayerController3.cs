using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float ySpeed;
    public float jumpForce;
    public Transform eyeCamera;
    public CharacterController controller;

    [Header("Shot")]
    public Transform firePoint;
    public GameObject bullet;

    [Header("Inputs")]
    public Vector3 moveInput;
    public Vector2 mouseInput;
    public Vector3 velocity;
    public int sensibility;
    public bool invertX, invertY;

    [Header("State")]
    public string STATE;
    public Abstract3 currentState;
    public Idle3 idle = new Idle3();
    public Run3 run = new Run3();
    public Walk3 walk = new Walk3();
    public Jump3 jump = new Jump3();

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    void Start()
    {
        currentState = idle;
    }

    void Update()
    {
        Vector3 verticalInput = transform.forward * Input.GetAxis("Vertical");
        Vector3 horizontalInput = transform.right * Input.GetAxis("Horizontal");

        moveInput = horizontalInput + verticalInput;
        moveInput.Normalize();
        moveInput *= speed;

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (controller.isGrounded)
        {
            ySpeed = -0.2f;
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpForce;
            }
        }

        velocity = moveInput;
        velocity.y = ySpeed;

        if (velocity.y > 0.1)
        {
            SwitchState(jump);
        }

        //Rotation - Mouse Input
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * sensibility;
        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if (invertY)
        {
            mouseInput.y = -mouseInput.y;
        }

        Rotation();

        currentState.LogicsUpdateState(this);

        STATE = currentState.ToString();
    }

    public void SwitchState(Abstract3 newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void Rotation()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, mouseInput.x, 0f));
        eyeCamera.rotation = Quaternion.Euler(eyeCamera.transform.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));
    }

    public void Movement()
    {
        controller.Move(velocity * Time.deltaTime);
    }

    public void Shot()
    {
        RaycastHit hit;
        if (Physics.Raycast(eyeCamera.position, eyeCamera.forward, out hit, 50f))
        {
            firePoint.LookAt(hit.point);
        }
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
