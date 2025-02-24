using System;
using Unity.Mathematics.Geometry;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    public float acceleration = 50f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 8f;
    public float jumpBoostForce = 8f;
    public bool isGrounded;

    bool jumpHeld;
    Animator animator;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void OnDrawGizmos()
    {
        Collider charCollider = GetComponent<Collider>();
        Vector3 center = charCollider.bounds.center;
        float halfDimY = (charCollider.bounds.extents.y) / 2f;

        bool hit = Physics.Raycast(center, Vector3.down, halfDimY + 0.1f);
        Gizmos.color = (hit) ? Color.red : Color.green;
        Gizmos.DrawLine(center, center + Vector3.down * (halfDimY + 0.1f));
    }

    void UpdateGroundContact()
    {
        Collider c = GetComponent<Collider>();
        Vector3 center = c.bounds.center;
        float halfDimY = c.bounds.extents.y;
        
        isGrounded = Physics.Raycast(center, Vector3.down, halfDimY + 0.1f);
    }
    
    void UpdateAnimation()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("In Air", !isGrounded);
    }

    void UpdateFaceDirection()
    {
        float axis = Input.GetAxis("Horizontal");
        if (axis != 0)
        {
            var facing = Mathf.Sign(axis);
            transform.rotation = Quaternion.Euler(0f, 90f * facing, 0f);
        }
    }

    void ClampVelocity()
    {
        Vector3 velocity = rb.linearVelocity;
        if (Mathf.Abs(velocity.x) > maxSpeed)
        {
            velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);
            rb.linearVelocity = velocity;
        }
    }

    void Update()
    {
        jumpHeld = Input.GetKey(KeyCode.Space);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpImpulse, ForceMode.VelocityChange);
        }
    }

    void FixedUpdate()
    {
        UpdateGroundContact();
        
        var axis = Input.GetAxis("Horizontal");
        
        if (isGrounded)
        {
            rb.linearVelocity += Vector3.right * (axis * acceleration * Time.fixedDeltaTime);
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, 0f);
            ClampVelocity();
        }
        else
        {
            float airMoveAcceleration = acceleration * 0.25f;
            rb.linearVelocity += Vector3.right * (axis * airMoveAcceleration * Time.fixedDeltaTime);
            ClampVelocity();
            
            if (jumpHeld && rb.linearVelocity.y > 0f)
            {
                rb.AddForce(Vector3.up * (jumpBoostForce * Time.fixedDeltaTime), ForceMode.VelocityChange);
            }
        }

        if (axis == 0f)
        {
            Vector3 dampedVelocity = rb.linearVelocity;
            dampedVelocity.x *= 1.0f - Time.fixedDeltaTime * 4f;
            rb.linearVelocity = dampedVelocity;
        }
        
        UpdateFaceDirection();
        UpdateAnimation();
    }
}
