using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HarvestSystem : MonoBehaviour
{
    [Header("UI References")]
    public Button takeQuizButton;

    [Header("Object Reference")]
    public GameObject sickle;
    public Animation sickleAnimation;
    public ParticleSystem sickleParticleSys;
    public AudioSource audioSource;

    private bool isHarvesting = false;
    private void OnEnable()
    {
        ToolFunction.CancelTools += CancelHarvest;
    }

    private void OnDisable()
    {
        ToolFunction.CancelTools -= CancelHarvest;
    }

    void Update()
    {
        if (isHarvesting)
        {
            // Left click = try harvest.
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    CropSystem crop = hit.collider.GetComponentInParent<CropSystem>();
                    if (crop != null)
                    {
                        if (crop.hasWilted || crop.IsMature)
                        {
                            crop.Harvest();
                            audioSource.Play();
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
