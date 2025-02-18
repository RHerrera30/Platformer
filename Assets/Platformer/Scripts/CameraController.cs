using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * (horizontal * panSpeed * Time.deltaTime));
    }
}
