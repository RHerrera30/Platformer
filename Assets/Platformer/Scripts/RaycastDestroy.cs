using UnityEngine;

public class RaycastDestroy : MonoBehaviour
{
    public Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPos = Input.mousePosition;
            Ray cursorRay = cam.ScreenPointToRay(screenPos);
            
            if (Physics.Raycast(cursorRay, out RaycastHit hit))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
