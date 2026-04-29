using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Transform PlayerObject;
    
    Player PlayerScript;
    Collider obj;

    float knockback = 3;
    Vector3 knockbackDir = Vector3.forward + (Vector3.up * 1.8f);

    void Start()
    {
        PlayerScript = PlayerObject.GetComponent<Player>();
        obj = GetComponentInChildren<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.body.CompareTag("Enemy"))
        {
            //knockback
            ContactPoint contact = collision.contacts[0];
            Vector3 newKnockback = (PlayerObject.rotation * knockbackDir) * knockback;
            collision.body.GetComponent<Rigidbody>().AddForceAtPosition(newKnockback, contact.point, ForceMode.Impulse);

            //custom actions
            collision.body.GetComponent<Enemy>().MeleeHit();
        }
    }

    private void SwingFinished()
    {
        PlayerScript.SwingCooldown(false);
    }

    private void SwapAwayFinished()
    {
        PlayerScript.SwapRangedIn();
        gameObject.SetActive(false);
    }

    private void SwapInFinished()
    {
        PlayerScript.SwingCooldown(false);
        obj.isTrigger = false;
    }

    private void ColliderOff()
    {
        obj.isTrigger = true;
    }
}
