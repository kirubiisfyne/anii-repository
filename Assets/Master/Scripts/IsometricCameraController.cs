using UnityEngine;

public class IsometricCameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public float zoomSpeed = 1.0f; // How fast zooming happens
    public float minZoom = 2.0f;   // Minimum orthographic size (zoom in limit)
    public float maxZoom = 10.0f;  // Maximum orthographic size (zoom out limit)
    public float panSpeed = 0.5f;  // How fast panning happens

    private Camera cam;
    private Vector3 lastMousePosition;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam.orthographic) 
        {
            cam.orthographic = true;
        }
    }

    void Update()
    {
        HandleZoom();
        HandlePan();
    }

    void HandleZoom()
    {
        // Zoom with mouse scroll wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            float newSize = cam.orthographicSize - scrollInput * zoomSpeed * 100 * Time.deltaTime;
            cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom); // Limit zoom range
        }
    }

    void HandlePan()
    {
        // Start panning when mouse is held down
        if (Input.GetMouseButtonDown(2)) // Middle mouse button
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(2)) // Drag while holding
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            Vector3 move = new Vector3(-delta.x * panSpeed, -delta.y * panSpeed, 0) * Time.deltaTime;
            transform.Translate(move, Space.Self); // Move camera in its local space
            lastMousePosition = Input.mousePosition;
        }
    }
}