using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ObjectSelector : MonoBehaviour
{
    private Camera cam;
    GameObject lastHovered;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Make a ray from mouse position
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject selected = hit.collider.gameObject;
                Debug.Log("Selected: " + selected.name);
            }
        }
    }
}
