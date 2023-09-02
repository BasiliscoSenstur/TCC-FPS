using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Moviment")]
    public CharacterController controller;
    public float moveSpeed;

    [Header("Camera")]
    public Transform eyeCamera;
    public int mouseSensibility;
    public bool invertX, invertY;

    //Inputs
    Vector3 moveInput;
    Vector2 mouseInput;

    void Start()
    {

    }
    void Update()
    {
        //moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.fixedDeltaTime;
        //moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.fixedDeltaTime;

        Vector3 verticalInput = transform.forward * Input.GetAxis("Vertical");
        Vector3 HorizontalInput = transform.right * Input.GetAxis("Horizontal");

        moveInput = verticalInput + HorizontalInput;
        moveInput.Normalize();

        //Rotation
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensibility;

        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if(invertY)
        {
            mouseInput.y = -mouseInput.y;
        }

        //mouseInput.Normalize();
        //mouseInput *= 2;

        controller.Move(moveInput * moveSpeed * Time.fixedDeltaTime);

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, mouseInput.x, 0f));
        eyeCamera.rotation = Quaternion.Euler(eyeCamera.transform.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));
    }
    void FixedUpdate()
    {

    }
}
