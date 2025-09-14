using System;
using System.Collections;
using System.Drawing;
using TMPro;
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

    public GameObject objectOperationRoot;

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

                OnToolSelected();
                ShowSelectedInfo();
            }
        }

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            HideSelectedInfo();
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

    private void ShowSelectedInfo()
    {
        ObjectOperationManager objectOperationManager = objectOperationRoot.GetComponent<ObjectOperationManager>();

        if (lastGameObject.CompareTag("Soil"))
        {
            objectOperationManager.button.GetComponentInChildren<Image>().color = new Color32(220, 104, 104, 255);
            objectOperationManager.button.GetComponentInChildren<TMP_Text>().text = "Level";
        }
        else if (lastGameObject.CompareTag("Crop"))
        {
            objectOperationManager.button.GetComponentInChildren<Image>().color = new Color32(220, 193, 104, 255);
            objectOperationManager.button.GetComponentInChildren<TMP_Text>().text = "Harvest";
        }

        if (GetSelectedData() == null) return;
        string[] infoString = GetSelectedData();

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(lastGameObject.transform.position);
        objectOperationRoot.GetComponent<RectTransform>().position = new Vector3(screenPosition.x, screenPosition.y + 256, screenPosition.z);

        objectOperationManager.TMPName.text = infoString[0];
        objectOperationManager.TMPDescription.text = infoString[1];

        objectOperationRoot.gameObject.SetActive(true);
    }

    private void HideSelectedInfo()
    {
        StartCoroutine(StartInfoHide());
    }

    private IEnumerator StartInfoHide()
    {
        objectOperationRoot.GetComponent<ObjectOperationManager>().GetComponent<Animation>().Play("anim_UIPannelOut");
        yield return new WaitForSecondsRealtime(0.25f);
        objectOperationRoot.gameObject.SetActive(false);
    }

    public void RemoveSoil()
    {
        HideSelectedInfo();

        if (lastGameObject.CompareTag("Soil"))
        {
            OnObjectRemoved?.Invoke(lastGameObject);
            lastGameObject.GetComponent<Soil>().OnRemoveSoil();
        }
        else if (lastGameObject.CompareTag("Crop"))
        {
            if (lastGameObject.GetComponent<CropSystem>().IsMature || lastGameObject.GetComponent<CropSystem>().hasWilted)
            {
                lastGameObject.GetComponent<CropSystem>().Harvest();
            }
            else
            {
                Debug.Log("Cannot be Harvested yet!");
                GameObject floatingText = Instantiate(GameManager.Instance.floatingText, GameManager.Instance.canvas.transform);
                floatingText.GetComponent<TMP_Text>().text = "Cannot be Harvested yet!";
            }
        }
    }

    public void OnToolSelected()
    {
        if (lastGameObject.GetComponentInParent<ToolFunction>() == null) return;

        lastGameObject.GetComponentInParent<ToolFunction>().UseTool();
    }

    private string[] GetSelectedData()
    {
        if (!GameManager.Instance.isHoldingTool)
        {
            if (lastGameObject.GetComponent<CropSystem>() != null)
            {
                CropData selectedCropData = lastGameObject.GetComponent<CropSystem>().cropData;

                string selectedName = selectedCropData.cropName;
                string selectedDescription = selectedCropData.cropDesciption;

                Debug.Log($"Crop Name: {selectedName}" + "\n" + $"Crop Description: {selectedDescription}");
                string[] infoString = { selectedName, selectedDescription };
                return infoString;
            }
            else if (lastGameObject.GetComponent<ObjectInfo>() != null)
            {
                Object selectedObject = lastGameObject.GetComponent<ObjectInfo>().objectData.objectInstance;

                string selectedName = selectedObject.Name;
                string selectedDescription = selectedObject.objectDescription;

                Debug.Log($"Object Name: {selectedName}" + "\n" + $"Object Description: {selectedDescription}");
                string[] infoString = { selectedName, selectedDescription };
                return infoString;
            }
        }
        return null;
    }
}
