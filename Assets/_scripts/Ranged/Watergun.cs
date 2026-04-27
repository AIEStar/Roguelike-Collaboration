using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Watergun : MonoBehaviour
{
    public Transform PlayerObject;
    public GameObject BallPrefab;
    public Transform NozzlePos;
    public Transform AimPos;
    
    Player PlayerScript;

    float knockback = 3;
    Vector3 knockbackDir = Vector3.forward + (Vector3.up * 1.8f);

    void Start()
    {
        PlayerScript = PlayerObject.GetComponent<Player>();
        gameObject.SetActive(false);
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
            collision.body.GetComponent<Enemy>().RangedHit();
        }
    }

    private void ShootFinished()
    {
        PlayerScript.ShootCooldown(false);
    }

    private void SwapAwayFinished()
    {
        PlayerScript.SwapMeleeIn();
        gameObject.SetActive(false);
    }

    private void SwapInFinished()
    {
        PlayerScript.ShootCooldown(false);
    }

    private void DispenseParticle()
    {
       GameObject newBall = Instantiate(BallPrefab, NozzlePos.position, Quaternion.LookRotation(AimPos.position - NozzlePos.position, Vector3.up));
        print("hi");
    }
}
