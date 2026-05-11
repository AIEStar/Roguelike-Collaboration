using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Patrol : Enemy
{
    const float knockbackStrength = 400;
    const float minDist = 0.5f;

    public GameObject RagdollPrefab;
    public Transform[] PatrolPositions;

    private GameObject ragdoll;
    private int patrolCount = 0;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, PatrolPositions[patrolCount].position) <= minDist)
        {
            patrolCount++;
            if(patrolCount >= PatrolPositions.Length)
            {
                patrolCount = 0;
            }
        }
        else
        {
            Vector3.AngleBetween
        }
    }

    public override void MeleeHit(Vector3 knockback, Vector3 point)
    {
        Ragdoll();
        ForceToRagdoll(knockback * knockbackStrength, point);
        Destroy(gameObject);
    }

    public override void RangedHit()
    {
        base.RangedHit();
    }

    void Ragdoll()
    {
        ragdoll = Instantiate(RagdollPrefab, transform.position, transform.rotation, transform.parent);
    }

    void ForceToRagdoll(Vector3 knockback, Vector3 point) 
    {
        //find closest body part to contact point 
        //Rigidbody[] parts = ragdoll.GetComponentsInChildren<Rigidbody>();
        //Rigidbody closest = parts[0];
        //float minDist = Mathf.Infinity;
        //foreach (Rigidbody t in parts)
        //{
        //    float dist = Vector3.Distance(t.gameObject.transform.position, point);
        //    if (dist < minDist)
        //    {
        //        closest = t;
        //        minDist = dist;
        //    }
        //}

        //aply force to body part
        PatrolRagdoll ragdollScript = ragdoll.GetComponent<PatrolRagdoll>();
        ragdollScript.chestrb.AddForce(knockback, ForceMode.Impulse);
    }
}
