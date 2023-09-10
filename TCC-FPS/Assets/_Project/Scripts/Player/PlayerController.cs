using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Movement")]
    public CharacterController controller;
    public Transform eyeCamera;
    public float speed;
    public float jumpForce;
    [HideInInspector] public float ySpeed;

    [Header("Animation")]
    public Animator anim;
    public string currentAnimation;

    [Header("Shot")]
    public Transform firePoint;
    public GameObject bullet;

    [Header("Inputs")]
    public int sensibility;
    public bool invertX, invertY;
    public Vector3 moveInput;
    public Vector2 mouseInput;
    public Vector3 velocity;

    [Header("State")]
    public string STATE;
    public Abstract currentState;
    public Idle idle = new Idle();
    public Run run = new Run();
    public Walk walk = new Walk();
    public Jump jump = new Jump();

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        instance = this;
    }
    void Start()
    {
        currentState = idle;
        currentState.EnterState(this);
    }

    void Update()
    {
        //Inputs
        Vector3 verticalInput = transform.forward * Input.GetAxis("Vertical");
        Vector3 horizontalInput = transform.right * Input.GetAxis("Horizontal");

        moveInput = horizontalInput + verticalInput;
        moveInput.Normalize();
        moveInput *= speed;

        //Gravity
        ySpeed += Physics.gravity.y * Time.deltaTime;

        //Jump
        if (controller.isGrounded)
        {
            ySpeed = -0.2f;
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpForce;
            }
        }
        else
        {
            SwitchState(jump);
        }

        velocity = moveInput;
        velocity.y = ySpeed;

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

    public void Rotation()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, mouseInput.x, 0f));
        eyeCamera.rotation = Quaternion.Euler(eyeCamera.transform.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));
    }

    public void Movement()
    {
        controller.Move(velocity * Time.deltaTime);
    }

    public void ChangeAnimation(string newAnimation)
    {
        if(currentAnimation == newAnimation)
        {
            return;
        }
        currentAnimation = newAnimation;
        anim.Play(currentAnimation);
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
