using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Transform camTarget;

    public Camera cam;
    public float fov, startFov, targetFov;
    public float aimSpeed;

    void Start()
    {
        cam = GetComponent<Camera>();
        startFov = 60;
    }
    public void Update()
    {
        targetFov = PlayerController.instance.activeGun.aimFov;

        if (PlayerController.instance.aim)
        {
            fov = Mathf.Lerp(fov, targetFov, PlayerController.instance.activeGun.aimSpeed);
        }
        else
        {
            fov = Mathf.Lerp(fov, startFov, PlayerController.instance.activeGun.aimSpeed);
        }

        cam.fieldOfView = fov;
    }
    void LateUpdate()
    {
        transform.position = camTarget.position;
        transform.rotation = camTarget.rotation;
    }
}
