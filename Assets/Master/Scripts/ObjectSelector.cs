using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ObjectSelector : MonoBehaviour
{
    private Camera cam;
    private GameObject lastHovered;
    private GameObject lastGameObject;
    private GridData gridData;

    public Vector3 lastPosition;
    public LayerMask groundLayer;

    public Image objectOperationRoot;

    public event Action OnClickedBuild, OnExitBuild;
    public event Action<GameObject> OnObjectRemoved;
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (IsPointerOverUI()) return;

            OnClickedBuild?.Invoke();

            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject selected = hit.collider.gameObject;
                lastGameObject = selected;
                Debug.Log("Selected: " + selected.name);

                OnSoilSelected();
            }
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            OnExitBuild?.Invoke();
            SelectedSoilExit();
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

    private void OnSoilSelected()
    {
        if (!lastGameObject.CompareTag("Soil")) return;

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(lastGameObject.transform.position);
        objectOperationRoot.rectTransform.position = new Vector3(screenPosition.x, screenPosition.y + 100, screenPosition.z);
        objectOperationRoot.gameObject.SetActive(true);
    }

    private void SelectedSoilExit()
    {
        objectOperationRoot.gameObject.SetActive(false);
    }

    public void RemoveSoil()
    {
        if (!lastGameObject.CompareTag("Soil")) return;

        SelectedSoilExit();

        OnObjectRemoved?.Invoke(lastGameObject);
        lastGameObject.GetComponent<Soil>().OnRemoveSoil();
    }
}
