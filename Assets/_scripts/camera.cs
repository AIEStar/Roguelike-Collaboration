using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform Target;
    Vector3 playerOffset;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        playerOffset = transform.position - Target.position;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(-mouseY, mouseX, 0);
    }

    void FixedUpdate()
    {
        //Vector3 targetPos = Target.position + playerOffset;
        //transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.1f);
    }
}
