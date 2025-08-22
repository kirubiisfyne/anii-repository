using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class HarvestSystem : MonoBehaviour
{
    [Header("UI References")]
    public Image harvestCursor;   // UI image for harvest tool
    public TMP_Text pointsText;
    public int score = 0;

    private bool isHarvesting = false;

    void Start()
    {
        harvestCursor.enabled = false;
    }

    void Update()
    {
        if (isHarvesting)
        {
            // Follow mouse
            harvestCursor.transform.position = Mouse.current.position.ReadValue();

            // Left click = try harvest
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    CropSystem crop = hit.collider.GetComponentInParent<CropSystem>();
                    if (crop != null && crop.IsMature)
                    {
                        crop.Harvest();
                        score += 10; // earn points
                        UpdateScoreUI();
                    }
                }
            }

            // Right click = cancel harvest mode
            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                CancelHarvest();
            }
        }
    }

    // Called by Harvest Button
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

    private void UpdateScoreUI()
    {
        if (pointsText != null)
            pointsText.text = score.ToString();
    }
}
