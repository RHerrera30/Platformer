using UnityEngine;

public class MarioController : MonoBehaviour
{
    public float acceleration = 3f;
    public float maxSpeed = 10f;
    public float jumpImpulse = 8f;
    public float jumpBoostForce = 8f;
    public float maxJumpSpeed = 8f;
    public float maxFallSpeed = 16f;
    public bool isGrounded;

    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAmount = Input.GetAxis("Horizontal");
        rb.linearVelocity += Vector3.right * (horizontalAmount * Time.deltaTime * acceleration);

        float horizontalSpeed = rb.linearVelocity.x;
        horizontalSpeed = Mathf.Clamp(horizontalSpeed, -maxSpeed, maxSpeed);

        Vector3 newVelocity = rb.linearVelocity;
        newVelocity.x = horizontalSpeed;
        rb.linearVelocity = newVelocity;
        
        Collider c = GetComponent<Collider>();
        Vector3 startPoint = transform.position;
        float castDistance = c.bounds.extents.y + 0.01f;
        
        isGrounded = Physics.Raycast(startPoint, Vector3.down, castDistance);
        
        Color color = (isGrounded) ? Color.green : Color.red;
        Debug.DrawLine(startPoint, startPoint + Vector3.down * castDistance, color, 0f, false);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpImpulse, ForceMode.VelocityChange);
        } else if (Input.GetKey(KeyCode.Space) && !isGrounded)
        {
            if (rb.linearVelocity.y > 0)
            {
                rb.AddForce(Vector3.up * jumpBoostForce, ForceMode.Acceleration);
            }
        }

        // float verticalSpeed = Mathf.Clamp(rb.linearVelocity.y,maxFallSpeed, maxJumpSpeed);
        // Vector3 clampedVelocity = rb.linearVelocity;
        // clampedVelocity.y = verticalSpeed;
        // rb.linearVelocity = clampedVelocity;

        if (horizontalAmount == 0f)
        {
            Vector3 decayedVelocity = rb.linearVelocity;
            decayedVelocity.x *= 1f - Time.deltaTime * 4f;
            rb.linearVelocity = decayedVelocity;
        }
        else
        {
            float yawRotation = (horizontalAmount > 0f) ? 90f : -90f;
            Quaternion rotation = Quaternion.Euler(0f, yawRotation, 0f);
            transform.rotation = rotation;
        }
    }
}
