using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float smoothAmount;
    
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, smoothAmount);
    }
}
