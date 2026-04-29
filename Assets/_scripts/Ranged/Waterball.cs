using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Waterball : MonoBehaviour
{
    const float power = 500;

    public GameObject ExplodeParticle; 
    public GameObject BallStart;
    public GameObject BallEnd;

    GameObject ball;
    GameObject end;
    GameObject hit;
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
            RaycastHit hit;
            float predictionDistance = rb.velocity.magnitude * 3;
            if (rb.SweepTest(transform.forward, out hit, predictionDistance))
            {
                PreHit(hit.distance / rb.velocity.magnitude);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        hit = Instantiate(ExplodeParticle, transform);
        hit.transform.rotation = dir;

        if (other.CompareTag("Enemy"))
        {
            //custom actions
            other.GetComponent<Enemy>().RangedHit();
        }

        StartCoroutine(DestroyAfterTime(hit.GetComponent<ParticleSystem>().main.duration));
    }

    void PreHit(float time)
    {
        prehit = true;
        Destroy(ball);
        ParticleSystem ps = BallEnd.GetComponent<ParticleSystem>();
        var main = ps.main;
        if(time < main.duration)
        {
            float spd = (main.duration / time);
            main.simulationSpeed = spd;
            ParticleSystem[] children = BallEnd.GetComponentsInChildren<ParticleSystem>();
            foreach (ParticleSystem child in children)
            {
                var childMain = child.main;
                childMain.simulationSpeed = spd;
            }
        }
        end = Instantiate(BallEnd, transform);
        end.transform.rotation = dir;
    }

    IEnumerator DestroyAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        Destroy(gameObject);
    }
}
