using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;
    public float propulsionForce;
    public float fireCooldown;
    bool readyToFire;

    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform respawnPad;
    public LayerMask whatIsLava;
    bool dead;
    
    public float bumperForce;
    public LayerMask whatIsBumper;
    bool touchBumper;
    
    public LayerMask whatIsFinish;
    bool finished;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
        readyToFire = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        dead = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsLava);
        touchBumper = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsBumper);
        finished = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsFinish);

        MyInput();
        SpeedControl();
        
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
        if (dead)
            respawn();

        if (touchBumper)
            Bumper();

        if (finished)
            SceneManager.LoadScene("TowerLevel");
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        if(Input.GetKey(KeyCode.Space) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        
        if (Input.GetMouseButtonDown(0) && readyToFire)
        {
            readyToFire = false;
            Fire();
            Invoke(nameof(ResetFire), fireCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    private void Fire()
    {
        Vector3 forward = Camera.main.transform.rotation * Vector3.forward;
        rb.AddForce(-forward * propulsionForce, ForceMode.VelocityChange);
    }
    private void ResetFire()
    {
        readyToFire = true;
    }

    private void Bumper()
    {
        rb.AddForce(transform.up * bumperForce, ForceMode.Impulse);
    }

    private void respawn()
    {
        Vector3 targetPosition = respawnPad.position;
        targetPosition.y += 2;
        rb.position = targetPosition;
    }
}