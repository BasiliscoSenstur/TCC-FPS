using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public CharacterController controller;
    public float moveSpeed;

    public Transform eyeCamera;
    public int sensibility;
    public bool invertX, invertY, canJump;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    Vector3 moveInput;
    Vector2 mouseInput;

    public float gravityModifier, jumpForce;

    void Start()
    {
        
    }

    void Update()
    {
        float yStored = moveInput.y;

        Vector3 verticalInput = transform.forward * Input.GetAxis("Vertical");
        Vector3 horizontalInput = transform.right * Input.GetAxis("Horizontal");

        moveInput = horizontalInput+ verticalInput;
        moveInput.Normalize();
        moveInput *= moveSpeed;

        moveInput.y = yStored;

        moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (controller.isGrounded)
        {
            moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
        }

        canJump = Physics.OverlapSphere(groundCheckPoint.position, 0.25f, whatIsGround).Length > 0;

        if (Input.GetButtonDown("Jump"))
        {
            if (canJump)
            {
                moveInput.y += jumpForce;
            }
        }

        controller.Move(moveInput * Time.deltaTime);

        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * sensibility;
        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if (invertY)
        {
            mouseInput.y = -mouseInput.y;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, mouseInput.x, 0f));
        eyeCamera.rotation = Quaternion.Euler(eyeCamera.transform.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));

    }
}
