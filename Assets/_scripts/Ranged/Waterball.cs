using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Waterball : MonoBehaviour
{
    const float power = 500;
    const float timeMin = 1.2f;

    public GameObject Hit; 
    public GameObject BallStart;
    public GameObject BallEnd;

    GameObject ball;
    GameObject end;
    GameObject hit;
    Quaternion dir;
    Rigidbody rb;
    bool prehit = false;
    float hitDuration;
    float endDuration;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ball = Instantiate(BallStart, transform);
        dir = Quaternion.Euler(transform.eulerAngles + new Vector3(90, 0, 0));
        ball.transform.rotation = dir;

        hitDuration = Hit.GetComponent<ParticleSystem>().main.duration;
        endDuration = BallEnd.GetComponent<ParticleSystem>().main.duration;

        rb.AddForce(transform.rotation * (Vector3.forward * power));
    }

    private void FixedUpdate()
    {
        if(!prehit)
        {
            float predictionDistance = rb.velocity.magnitude * endDuration;
            if (rb.SweepTest(transform.forward, out RaycastHit hit, predictionDistance))
            {
                PreHit(hit.distance / rb.velocity.magnitude);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(ball);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        hit = Instantiate(Hit, transform);
        hit.transform.rotation = dir;

        if (other.CompareTag("Enemy"))
        {
            //custom actions
            other.GetComponent<Enemy>().RangedHit();
        }

        StartCoroutine(DestroyAfterTime(hitDuration, gameObject));
    }

    void PreHit(float time)
    {
        prehit = true;
        if(time < timeMin)
        {
            return;
        }
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
        DestroyAfterTime(timeMin / 2, ball);

        StartCoroutine(DestroyAfterTime(hitDuration + time, gameObject));
    }

    IEnumerator DestroyAfterTime(float seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);

        Destroy(obj);
    }
}
