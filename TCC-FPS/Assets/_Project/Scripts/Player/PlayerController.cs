using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Movement")]
    public Transform eyeCamera;
    public CharacterController controller;
    public float speed, jumpForce;
    float ySpeed;

    [Header("Shot")]
    public Transform firePoint;
    public GameObject bullet;

    [Header("Inputs")]
    public Vector3 moveInput;
    public Vector2 mouseInput;
    public Vector3 velocity;
    public int sensibility;
    public bool invertX, invertY;

    [Header("Animations")]
    Animator anim;
    public string currentAnimation;

    [Header("State")]
    public string STATE;
    public Abstract currentState;
    public Idle idle = new Idle();
    public Run run = new Run();
    public Walk walk = new Walk();
    public Jump jump = new Jump();

    private void Awake()
    {
        instance = this;
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
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

    public void SwitchState(Abstract newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
    public void ChangeAnimation(string newAnimation)
    {
        if (currentAnimation == newAnimation)
        {
            return;
        }
        anim.Play(newAnimation);
        currentAnimation = newAnimation;
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
