using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public Transform camTarget;
    void Start()
    {
        
    }
    void LateUpdate()
    {
        transform.position = camTarget.position;
        transform.rotation = camTarget.rotation;
    }
}
