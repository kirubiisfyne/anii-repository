using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ObjectSelector : MonoBehaviour
{
    private Camera cam;
    private GameObject lastHovered;

    public Vector3 lastPosition;
    public LayerMask groundLayer;

    public event Action OnClickedBuild, OnExitBuild;
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnClickedBuild?.Invoke();

            // Make a ray from mouse position
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject selected = hit.collider.gameObject;
                Debug.Log("Selected: " + selected.name);
            }
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            OnExitBuild?.Invoke();
        }
    }

    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();
    public Vector3 GetSelectedMousePosition()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;
        Ray ray = cam.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, groundLayer))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}
