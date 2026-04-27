using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Waterball : MonoBehaviour
{
    const float power = 500;

    public GameObject ExplodeParticle; 
    public GameObject BallStart;
    public GameObject BallEnd;

    GameObject ball;
    Quaternion dir;
    Rigidbody rb;
    bool prehit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ball = Instantiate(BallStart, transform);
        dir = Quaternion.Euler(transform.eulerAngles + new Vector3(90, 0, 0));
        ball.transform.rotation = dir;

        rb.AddForce(transform.rotation * (Vector3.forward * power));
    }

    private void FixedUpdate()
    {
        if(!prehit)
        {
            float predictionDistance = rb.velocity.magnitude * Time.fixedDeltaTime * 3;
            if (rb.SweepTest(transform.forward, out _, predictionDistance))
            {
                PreHit();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        GameObject hit = Instantiate(ExplodeParticle, transform);
        hit.transform.rotation = dir;
    }

    public void PreHit()
    {
        prehit = true;
        print("pre");
        Destroy(ball);
        GameObject end = Instantiate(BallEnd, transform);
        end.transform.rotation = dir;
    }
}
