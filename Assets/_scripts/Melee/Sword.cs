using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Transform PlayerObject;
    
    Player PlayerScript;
    Collider[] objCollider;
    MeshRenderer[] mesh;

    Vector3 knockbackDir = (Vector3.forward * 0.4f) + Vector3.up;

    void Start()
    {
        PlayerScript = PlayerObject.GetComponent<Player>();
        objCollider = GetComponentsInChildren<Collider>();
        mesh = GetComponentsInChildren<MeshRenderer>();
        
        ColliderOff();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //knockback
            ContactPoint contact = collision.contacts[0];
            Vector3 newKnockback = PlayerObject.rotation * knockbackDir;

            //custom actions
            var enemy = collision.body.GetComponent<Enemy>();
            enemy.MeleeHit(newKnockback, contact.point);
            //var enemy = collision.body.GetComponent<Enemy>();
            //if(enemy.GetType() == typeof(Dummy)) {}
        }
    }

    private void SwingFinished()
    {
        PlayerScript.SwingCooldown(false);
    }

    private void SwapAwayFinished()
    {
        MeshDisabled();
        PlayerScript.SwapRangedIn();
    }

    public void SwapInFinished()
    {
        PlayerScript.SwingCooldown(false);
        ColliderOn();
    }

    private void ColliderOff()
    {
        foreach (Collider c in objCollider)
        {
            c.isTrigger = true;
        }
    }

    private void ColliderOn()
    {
        foreach (Collider c in objCollider)
        {
            c.isTrigger = false;
        }
    }

    private void MeshEnabled()
    {
        foreach (MeshRenderer m in mesh)
        {
            m.enabled = true;
        }
    }

    private void MeshDisabled()
    {
        foreach (MeshRenderer m in mesh)
        {
            m.enabled = false;
        }
    }
}
