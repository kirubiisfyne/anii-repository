using System.Collections;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HarvestSystem : MonoBehaviour
{
    [Header("UI References")]
    public Image harvestCursor;   // UI image for harvest tool
    public Button takeQuizButton;
    public AudioSource audioSource;

    private bool isHarvesting = false;

    [Header("Object Reference")]
    public GameObject sickle;
    public Animation sickleAnimation;
    public ParticleSystem sickleParticleSys;
    private void OnEnable()
    {
        ToolFunction.CancelTools += CancelHarvest;
    }

    private void OnDisable()
    {
        ToolFunction.CancelTools -= CancelHarvest;
    }
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
        sickle.SetActive(true);
    }

    public void CancelHarvest()
    {
        if (isHarvesting)
        {
            StartCoroutine(StartCancelHarvest());
        }
    }

    private IEnumerator StartCancelHarvest()
    {
        isHarvesting = false;

        sickleAnimation.Play("anim_toolOut");
        sickleParticleSys.Play();
        yield return new WaitForSecondsRealtime(0.25f);
        sickle.SetActive(false);
    }
}
