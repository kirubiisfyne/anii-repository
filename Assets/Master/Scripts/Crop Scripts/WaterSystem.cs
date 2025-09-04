using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class WaterSystem : MonoBehaviour
{
    [Header("UI References")]
    public Image waterCursor;   // UI image for water tool
    public AudioSource audioSource;

    private bool isWatering = false;

    void Start()
    {
        waterCursor.enabled = false;
    }

    void Update()
    {
        if (isWatering)
        {
            // Follow mouse.
            waterCursor.transform.position = Mouse.current.position.ReadValue();

            // Left click = try water.
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    CropSystem crop = hit.collider.GetComponentInParent<CropSystem>();
                    if (crop != null)
                    {
                        crop.Water();
                        if (!crop.wasWatered)
                        {
                            audioSource.Play();
                        }
                    }
                }
            }

            // Right click = cancel water mode.
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                CancelWater();
            }
        }
    }

    // Called by Water Button.
    public void ActivateWater()
    {
        isWatering = true;
        waterCursor.enabled = true;
    }

    private void CancelWater()
    {
        isWatering = false;
        waterCursor.enabled = false;
    }
}
