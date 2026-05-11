using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    const float knockbackStrength = 8;

    public float health;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public virtual void MeleeHit(Vector3 knockback, Vector3 point)
    {
        GetComponent<Rigidbody>().AddForceAtPosition(knockback * knockbackStrength, point, ForceMode.Impulse);
    }

    public virtual void RangedHit()
    {

    }
}
