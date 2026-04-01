using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    const float wobbleSpeedIdle = 3;
    const float wobbleSpeedMoving = 7;
    const int wobbleDivisorMoving = 50;
    const int wobbleDivisorIdle = 120;
    const float wobbleMaxY = 0.03f;
    const float meleeCooldown = 3.5f;

    float animationTimer = -1;
    int animationTimerDir = 1;
    float meleeCooldownTimer = 0;
    float animationWobbleDivisor = 50;
    bool animate = true;
    bool swing = false;
    bool shoot = false;
    bool melee = true;
    
    CharacterController character;

    public float animationSpeed = 1;
    public float movementSpeed;

    public Transform RHWeapon;
    public Animator SwordAnimator;

    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        //  animation
        animationTimer += Time.deltaTime * animationTimerDir * animationSpeed;
        if (animationTimer * animationTimerDir >= 1f)
        {
            animationTimer = animationTimerDir;
            animationTimerDir = 0 - animationTimerDir;
        }

        if (animate)
        {
            RHWeapon.localPosition = new(RHWeapon.localPosition.x, RHWeapon.localPosition.y + (Mathf.Asin(animationTimer) / (100 * animationWobbleDivisor)), RHWeapon.localPosition.z);
            if (Mathf.Abs(RHWeapon.localPosition.y) > wobbleMaxY)
            {
                RHWeapon.localPosition = new(RHWeapon.localPosition.x, Mathf.Sign(RHWeapon.localPosition.y) * wobbleMaxY, RHWeapon.localPosition.z);
            }
        }

        //  movement
        float horzInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        Vector3 moveDir = (horzInput * Vector3.right) + (vertInput * Vector3.forward);

        character.Move(moveDir * movementSpeed * Time.deltaTime);

        if(horzInput == 0 && vertInput == 0)
        {
            animationWobbleDivisor = wobbleDivisorIdle;
            animationSpeed = wobbleSpeedIdle;
        }
        else
        {
            animationWobbleDivisor = wobbleDivisorMoving;
            float moveFactor = Mathf.Abs(horzInput) + Mathf.Abs(vertInput);
            animationSpeed = wobbleSpeedMoving + (((wobbleSpeedMoving - wobbleSpeedIdle) / 3) * Mathf.Max(0f, moveFactor - 1));
        }

        //  melee
        if (Input.GetMouseButtonDown(0) && !swing)
        {
            SwingCooldown(true);

            if(!melee)
            {
                melee = true;
                shoot = true;
            } 
            else
            {
                SwordAnimator.SetTrigger("Swing");
            }
        }

        //ranged
        if (Input.GetMouseButtonDown(1) && !shoot)
        {
            ShootCooldown(true);

            if (melee)
            {
                melee = false;
                swing = true;
                SwordAnimator.SetTrigger("SwapAway");
            }
            else
            {
                SwordAnimator.SetTrigger("Swing");
            }
        }
    }

    public void SwingCooldown(bool state)
    {
        swing = state;
        animate = !state;
    }

    public void ShootCooldown(bool state)
    {
        shoot = state;
        animate = !state;
    }

    public void SwapMeleeIn()
    {
        SwingCooldown(true);
        ShootCooldown(true);
        SwordAnimator.SetTrigger("SwapIn");
    }

    public void SwapRangedIn()
    {
        SwingCooldown(true);
        ShootCooldown(true);
    }
}
