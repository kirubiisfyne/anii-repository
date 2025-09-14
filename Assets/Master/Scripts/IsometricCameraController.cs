using UnityEngine;

public class IsometricCameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public float zoomSpeed = 1.0f;
    public float minZoom = 2.0f;
    public float maxZoom = 10.0f;
    public float panSpeed = 0.5f;
    public float smoothReturnSpeed = 3f;

    [Space(15)]
    public Camera overlayCam;
    private Camera cam;

    private Vector3 lastMousePosition;
    private Vector3 basePosition;

    private bool returningToBase = false;

    [Header("Pan Limits (at max zoom)")]
    public float minX = -20f;
    public float maxX = 20f;
    public float minZ = -20f;
    public float maxZ = 20f;

    [Range(0f, 1f)]
    public float minPanFraction = 0.2f;
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographic = true;

        basePosition = transform.position;
    }

    void Update()
    {
        HandleZoom();
        HandlePan();
        HandleReturnToBase();
        SyncOverlay();
    }

    void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            float newSize = cam.orthographicSize - scrollInput * zoomSpeed * 100 * Time.deltaTime;
            cam.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);

            if (cam.orthographicSize < maxZoom)
                returningToBase = false;
        }

        if (cam.orthographicSize >= maxZoom && !Input.GetMouseButton(1)) // right click not held
            returningToBase = true;
    }

    void HandlePan()
    {
        if (cam.orthographicSize < maxZoom && !returningToBase)
        {
            if (Input.GetMouseButtonDown(1)) // right-click to start drag
            {
                lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(1))
            {
                Vector3 delta = Input.mousePosition - lastMousePosition;

                // world-space iso directions
                Vector3 right = new Vector3(1, 0, -1).normalized;
                Vector3 up = new Vector3(1, 0, 1).normalized;

                // drag movement
                Vector3 move = (-delta.x * right + -delta.y * up) * panSpeed * Time.deltaTime;

                transform.position += move;

                ClampPan();

                lastMousePosition = Input.mousePosition;
            }
        }
    }

    void HandleReturnToBase()
    {
        if (returningToBase)
        {
            transform.position = Vector3.Lerp(transform.position, basePosition, Time.deltaTime * smoothReturnSpeed);

            if (Vector3.Distance(transform.position, basePosition) < 0.1f)
            {
                transform.position = basePosition;
                returningToBase = false;
            }
        }
    }

    void ClampPan()
    {
        // clamp while not returning
        // compute how far we are between minZoom (0) and maxZoom (1)
        float zoomPercent = Mathf.Clamp01((cam.orthographicSize - minZoom) / (maxZoom - minZoom));

        // base (center) of the allowed pan area (set at Start)
        float baseX = basePosition.x;
        float baseZ = basePosition.z;

        // full half-ranges from base to edges
        float leftRange = Mathf.Abs(baseX - minX);
        float rightRange = Mathf.Abs(maxX - baseX);
        float downRange = Mathf.Abs(baseZ - minZ);
        float upRange = Mathf.Abs(maxZ - baseZ);

        // allowed limits at minimum zoom: small box around base
        float minX_atMinZoom = baseX - leftRange * minPanFraction;
        float maxX_atMinZoom = baseX + rightRange * minPanFraction;
        float minZ_atMinZoom = baseZ - downRange * minPanFraction;
        float maxZ_atMinZoom = baseZ + upRange * minPanFraction;

        // interpolate from the tight min-zoom limits to the full max-zoom limits
        float scaledMinX = Mathf.Lerp(minX_atMinZoom, minX, zoomPercent);
        float scaledMaxX = Mathf.Lerp(maxX_atMinZoom, maxX, zoomPercent);
        float scaledMinZ = Mathf.Lerp(minZ_atMinZoom, minZ, zoomPercent);
        float scaledMaxZ = Mathf.Lerp(maxZ_atMinZoom, maxZ, zoomPercent);

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, scaledMinX, scaledMaxX);
        pos.z = Mathf.Clamp(pos.z, scaledMinZ, scaledMaxZ);
        transform.position = pos;
    }

    void SyncOverlay()
    {
        if (overlayCam != null)
        {
            overlayCam.orthographicSize = cam.orthographicSize;
            overlayCam.transform.position = cam.transform.position;
            overlayCam.transform.rotation = cam.transform.rotation;
        }
    }
}
