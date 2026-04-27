using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float movementSpeed;
    public float walkSpeed = 5f;
    public float jumpHeight = 2f;
    public float runSpeed = 2f;
    
    //To increase gravity speed while falling
    public float fallGravityMultiplyer = 2f;
    public float mouseSensitivity = 2.0f;
    public float pitchRange = 60.0f;
    public float grouundPointVelocity = -10.0f;
    public float groundGravityVelocity = -8.0f;
 

    private float forwardInputValue;
    private float strafeInputValue;
    private bool jumpInput;
    
  
    private bool groundPointInput;

 
    //Physics fall velocity
    private float terminalVelocity = 53f;
    private float verticalVelocity;

    private float rotateCameraPitch;

        private Camera firstPresonCam;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        firstPresonCam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;

        movementSpeed = walkSpeed;
    }
 
    void Update()
    {
        forwardInputValue = Input.GetAxisRaw("Vertical");
        strafeInputValue = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown("z"))
        {
            if (characterController.isGrounded == true)
            {
                movementSpeed = runSpeed;

                if (Input.GetKeyUp("z"))
                {
                    movementSpeed = walkSpeed;
                }
            }


        }
        if (Input.GetButton("Jump"))
        {
            if (characterController.isGrounded == true)
            {
                jumpInput = Input.GetButtonDown("Jump");
            }
            else
            {
                 groundPointInput = Input.GetButtonDown("Jump");
            }
        }


        Movement();
        JumpAndGravity();
        CameraMovement();
    }

    void Movement()
    {
        Vector3 direction = (transform.forward * forwardInputValue + transform.right * strafeInputValue).normalized * movementSpeed * Time.deltaTime;
        characterController.Move(direction);

        // add physics using Vector3s up direction (World Coordinates) aa the direction of gravity
        direction += Vector3.up * verticalVelocity * Time.deltaTime;
        characterController.Move(direction);
    }

    async Task JumpAndGravity()
    {
        if (characterController.isGrounded)
        {
            // stop velocity dropping infinetly when grounded
            if (verticalVelocity < 0.0f)
            {
                verticalVelocity = groundGravityVelocity;
            }


            if (jumpInput)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
                await Task.Delay(TimeSpan.FromSeconds(0.3));
                if(Input.GetButton("Jump"))
                {
                    verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
                }
            }

            
        }
        else
        {
            if (verticalVelocity < -52.99)
            {
                verticalVelocity = -2;
            }
            //Apply gravity over time if under terminal gravity
            if (verticalVelocity < terminalVelocity)
            {
                //Set gravity multiplier if falling downwards
                float gravityMultiplier = 1;
                if (characterController.velocity.y < -1)
                {
                    gravityMultiplier = fallGravityMultiplyer;
                }
                verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime * 1.5f;

              
                if (groundPointInput)
                {
                    verticalVelocity = grouundPointVelocity;
                }
            }
           
        }
    }

    void CameraMovement()
    {
        // rotate the player around
        float rotateYaw = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotateYaw, 0);

        // rotate the camera up and down
        rotateCameraPitch += -Input.GetAxis("Mouse Y") * mouseSensitivity;
        //Lock the rotation so camera does not flip
        rotateCameraPitch = Mathf.Clamp(rotateCameraPitch, -pitchRange, pitchRange);
        firstPresonCam.transform.localRotation = Quaternion.Euler(rotateCameraPitch, 0, 0);
    }
}
