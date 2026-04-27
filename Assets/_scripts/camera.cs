using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform Target;
    public Player PlayerScript;
    Vector3 playerOffset;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
        playerOffset = transform.position - Target.position;
        Collide(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            PlayerScript.EnemySpotted(1);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            PlayerScript.EnemySpotted(-1);
        }
    }

    public void Collide(bool state)
    {
        GetComponent<Collider>().enabled = state;
    }
}
