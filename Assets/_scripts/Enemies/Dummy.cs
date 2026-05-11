using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Dummy : Enemy
{
    public Material hitMaterial;
    public Material hitMaterial2;
    public Material normMaterial;

    MeshRenderer render;
    Vector3 startPos;
    Quaternion startRot;
    bool reset = true;
    bool resetting = false;

    private void Start()
    {
        render = GetComponent<MeshRenderer>();
        startPos = transform.position;
        startRot = transform.rotation;
    }

    public override void MeleeHit(Vector3 k, Vector3 p)
    {
        base.MeleeHit(k, p);
        render.material = hitMaterial;
    }

    public override void RangedHit()
    {
        render.material = hitMaterial2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12)
        {
            if(!resetting)
            {
                if(!reset)
                {
                    StopCoroutine(nameof(ReturnAfterTime));
                    StartCoroutine(ReturnAfterTime(3));
                    resetting = true;
                }
                reset = !reset;
            }
        }
    }

    IEnumerator ReturnAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.angularVelocity = Vector3.zero;
        transform.SetPositionAndRotation(startPos, startRot);
        rb.isKinematic = false;

        resetting = false;
    }
}
