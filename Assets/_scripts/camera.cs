using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class camera : MonoBehaviour
{
    public Transform Target;
    Vector3 playerOffset;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        playerOffset = Target.position - transform.position;
    }

    void FixedUpdate()
    {
        Vector3 targetPos = Target.position + playerOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.1f);
    }
}
