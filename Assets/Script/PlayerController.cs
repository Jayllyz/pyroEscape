using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform tr;
    
    public float propulsionForce = 15.0f;
    public float walkForce = 10.0f;
    public float maxSpeed = 15.0f;
    
    public float jumpForce = 10.0f;
    bool readyToJump;
    public float airMultiplier;
    public float jumpCooldown;
    
    public float groundDrag;
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    
    private void Start()
    {
        readyToJump = true;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 forward = Camera.main.transform.rotation * Vector3.forward;
            rb.AddForce(-forward * propulsionForce, ForceMode.VelocityChange);
        }

        if (grounded)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                rb.AddForce(tr.rotation * Vector3.forward * walkForce, ForceMode.Acceleration);
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(tr.rotation * Vector3.back * walkForce, ForceMode.Acceleration);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                rb.AddForce(tr.rotation * Vector3.left * walkForce, ForceMode.Acceleration);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(tr.rotation * Vector3.right * walkForce, ForceMode.Acceleration);
            }
        } else if (!grounded)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                rb.AddForce(tr.rotation * Vector3.forward * walkForce * airMultiplier, ForceMode.Acceleration);
            }

            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(tr.rotation * Vector3.back * walkForce * airMultiplier, ForceMode.Acceleration);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                rb.AddForce(tr.rotation * Vector3.left * walkForce * airMultiplier, ForceMode.Acceleration);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(tr.rotation * Vector3.right * walkForce * airMultiplier, ForceMode.Acceleration);
            }
        }

        // vitesse capped
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        
        // pour rajouter de la friction au mouvement au sol
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
        
        if (Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(resetJump), jumpCooldown);
        }
    }
    
    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    
    private void resetJump()
    {
        readyToJump = true;
    }
}
