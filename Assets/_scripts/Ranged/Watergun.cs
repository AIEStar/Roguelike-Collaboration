using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Watergun : MonoBehaviour
{
    public Transform PlayerObject;
    public GameObject BallPrefab;
    public Transform NozzlePos;
    public LayerMask mask;
    
    Player PlayerScript;
    MeshRenderer mesh;

    float knockback = 3;
    Vector3 knockbackDir = Vector3.forward + (Vector3.up * 1.8f);

    void Start()
    {
        PlayerScript = PlayerObject.GetComponent<Player>();
        mesh = GetComponentInChildren<MeshRenderer>();
        mesh.enabled = false;
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
        mesh.enabled = false;
        PlayerScript.SwapMeleeIn();
    }

    private void SwapInFinished()
    {
        PlayerScript.ShootCooldown(false);
    }

    private void DispenseParticle()
    {
        if(Physics.Raycast(PlayerScript.cam.transform.position, PlayerScript.cam.transform.forward, out RaycastHit info, 1000, ~mask))
        {
            Instantiate(BallPrefab, NozzlePos.position, Quaternion.LookRotation(info.point - NozzlePos.position, Vector3.up));
        }
    }

    private void MeshEnabled()
    {
        mesh.enabled = true;
    }
}
