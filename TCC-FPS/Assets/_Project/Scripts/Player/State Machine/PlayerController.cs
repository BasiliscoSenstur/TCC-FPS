using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
    }
    //---------------------------------------//
    [Header("Movement")]
    public CharacterController controller;
    public float moveSpeed;
    public float jumpForce;
    public Transform eyeCamera;

    public float gravityModifier;

    public bool canJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    //Shot
    public Transform firePoint;
    public GameObject bullet;

    [Header("Inputs")]
    public Vector3 moveInput;
    public Vector2 mouseInput;
    public int sensibility;
    public bool invertX, invertY;

    [Header("Animations")]
    Animator anim;
    public string currenAnimation;

    [Header("State")]
    public string STATE;
    public string TESTE;
    public Abstract currentState;
    public IdleState idle = new IdleState();
    public RunningState running = new RunningState();
    public WalkingState walking = new WalkingState();
    public JumpingState jumping = new JumpingState();

    public void Start()
    {
        anim = GetComponent<Animator>();
        currentState = idle;
        currentState.EnterState(this);
    }

    public void Update()
    {
        float yStored = moveInput.y;

        ////Movement Inputs
        Vector3 verticalInput = transform.forward * Input.GetAxis("Vertical");
        Vector3 horizontalInput = transform.right * Input.GetAxis("Horizontal");

        moveInput = horizontalInput + verticalInput;
        moveInput.Normalize();
        moveInput *= moveSpeed;

        moveInput.y = yStored;

        //Gravity
        if (currentState == jumping)
        {
            moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;
        }
        else
        {
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        ////Jump
        canJump = Physics.OverlapSphere(groundCheckPoint.position, 0.25f, whatIsGround).Length > 0;
        if (!canJump)
        {
            SwitchState(jumping);
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
        TESTE = jumping.run.ToString();
    }

    public void SwitchState(Abstract newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
    public void ChangeAnimation(string newAnimation)
    {
        if (currenAnimation == newAnimation)
        {
            return;
        }
        anim.Play(newAnimation);
        currenAnimation = newAnimation;
    }
    public void Movement()
    {
        controller.Move(moveInput * Time.deltaTime);
    }
    public void Rotation()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, mouseInput.x, 0f));
        eyeCamera.rotation = Quaternion.Euler(eyeCamera.transform.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));
    }
    public void Jump()
    {
        moveInput.y += jumpForce;
        moveInput.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);

        controller.Move(moveInput);
    }
    public void Shot()
    {
        RaycastHit hit;
        if(Physics.Raycast(eyeCamera.position, eyeCamera.forward,out hit, 50f))
        {
            firePoint.LookAt(hit.point);
        }
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
