using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHealth : MonoBehaviour
{
    public Transform camera;
    public void LateUpdate()
    {
        transform.LookAt(transform.position + camera.forward);
    }
}
