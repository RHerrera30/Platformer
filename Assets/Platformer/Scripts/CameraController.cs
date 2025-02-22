using UnityEngine;

public class CameraController : MonoBehaviour
{
    // public float panSpeed = 10f;

    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // float horizontal = Input.GetAxis("Horizontal");
        // transform.Translate(Vector3.right * (horizontal * panSpeed * Time.deltaTime));
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
