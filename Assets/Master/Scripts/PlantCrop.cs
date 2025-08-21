using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlantCrop : MonoBehaviour
{
    [Header("UI References")]
    public Image cursorImage;  // Follows the mouse

    private CropData selectedCrop;
    private bool isHoldingCrop = false;

    void Start()
    {
        cursorImage.enabled = false;
    }

    void Update()
    {
        if (isHoldingCrop)
        {
            // Cursor follows mouse
            cursorImage.transform.position = Mouse.current.position.ReadValue();

            // Left click = try plant
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Soil soil = hit.collider.GetComponent<Soil>();
                    if (soil != null && CanPlantOnSoil(soil))
                    {
                        if (soil.TryPlant(selectedCrop.cropPrefab, hit.point))
                            ReleaseCrop();
                    }
                }
            }

            // Right click = cancel
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                ReleaseCrop();
            }
        }
    }

    public void PickCrop(CropData crop)
    {
        selectedCrop = crop;
        cursorImage.sprite = crop.cursorSprite;
        cursorImage.enabled = true;
        isHoldingCrop = true;
    }

    private void ReleaseCrop()
    {
        selectedCrop = null;
        cursorImage.enabled = false;
        isHoldingCrop = false;
    }

    private bool CanPlantOnSoil(Soil soil)
    {
        foreach (var type in selectedCrop.allowedSoils)
        {
            if (soil.soilType == type)
                return true;
        }
        return false;
    }
}
