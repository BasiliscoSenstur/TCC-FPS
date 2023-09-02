using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public CharacterController controller;
    public Transform eyeCamera;
    public float moveSpeed, jumpForce, gravityMultiplier;
    public bool canJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    [Header("Inputs")]
    public Vector3 moveInput;
    public Vector2 mouseInput;
    public int sensibility;
    public bool invertX, invertY;

    [Header("State")]
    public string STATE;
    public Abstract currentState;
    public IdleState idleState = new IdleState();
    public RunningState runningState = new RunningState();
    public JumpingState jumpingState = new JumpingState();

    public void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }

    public void Update()
    {
        //Movement Inputs
        Vector3 verticalInput = transform.forward * Input.GetAxis("Vertical");
        Vector3 horizontalInput = transform.right * Input.GetAxis("Horizontal");

        moveInput = horizontalInput + verticalInput;
        moveInput.Normalize();
        moveInput *= moveSpeed;

        float yStored = moveInput.y;

        moveInput.y += Physics.gravity.y * gravityMultiplier * Time.deltaTime;

        if (controller.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityMultiplier * Time.deltaTime;
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

        //Jump
        canJump = Physics.OverlapSphere(groundCheckPoint.position, 0.25f, whatIsGround).Length > 0;

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        currentState.LogicsUpdateState(this);

        STATE = currentState.ToString();
    }

    public void FixedUpdate()
    {
        currentState.PhysicsUpdateState(this);
    }

    public void SwitchState(Abstract newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
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
        SwitchState(jumpingState);
    }
}
