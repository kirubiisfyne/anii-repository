using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class HarvestSystem : MonoBehaviour
{
    [Header("UI References")]
    public Image harvestCursor;   // UI image for harvest tool
    public Button takeQuizButton;
    public AudioSource audioSource;

    private bool isHarvesting = false;

    void Start()
    {
        harvestCursor.enabled = false;
    }

    void Update()
    {
        if (isHarvesting)
        {
            // Follow mouse.
            harvestCursor.transform.position = Mouse.current.position.ReadValue();

            // Left click = try harvest.
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    CropSystem crop = hit.collider.GetComponentInParent<CropSystem>();

                    if (crop == null) return;

                    if (crop.hasWilted || crop.IsMature)
                    {
                        crop.Harvest();
                        audioSource.Play();

                        // **To be fixed**
                        if (PointsEXPSystem.Instance.CurrentPoints >= PointsEXPSystem.Instance.pointThreshold)
                        {
                            takeQuizButton.interactable = true;
                        }
                    }
                }
            }

            // Right click = cancel harvest mode.
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                CancelHarvest();
            }
        }
    }

    // Called by Harvest Button.
    public void ActivateHarvest()
    {
        isHarvesting = true;
        harvestCursor.enabled = true;
    }

    private void CancelHarvest()
    {
        isHarvesting = false;
        harvestCursor.enabled = false;
    }
}
