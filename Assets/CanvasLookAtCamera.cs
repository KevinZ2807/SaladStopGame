using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLookAtCamera : MonoBehaviour
{
    public Camera playerCamera;
    
    void Update()
    {
        if (playerCamera != null)
        {
            transform.LookAt(transform.position + playerCamera.transform.rotation * Vector3.forward,
                playerCamera.transform.rotation * Vector3.up);
        }
    }
}
