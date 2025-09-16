using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;

public class WaterSystem : MonoBehaviour
{
    [Header("UI References")]
    public Button WaterButton;

    [Header("Object Reference")]
    public GameObject bucket;
    public Animation bucketAnimation;
    public ParticleSystem bucketParticleSys;
    public AudioSource audioSource;

    private bool isWatering = false;

    private void OnEnable()
    {
        ToolFunction.CancelTools += CancelWater;
    }

    private void OnDisable()
    {
        ToolFunction.CancelTools -= CancelWater;
    }

    void Update()
    {
        if (isWatering)
        {
            // Left click = try water.
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    CropSystem crop = hit.collider.GetComponentInParent<CropSystem>();
                    if (crop != null)
                    {
                        if (!crop.wasWatered && !crop.hasWilted)
                        {
                            crop.Water();
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
        WaterButton.interactable = false;

        isWatering = true;
        GameManager.Instance.isHoldingTool = true;
        bucket.SetActive(true);
    }

    public void CancelWater()
    {
        if (isWatering)
        {
            StartCoroutine(StartCancelWater());
        }
    }

    private IEnumerator StartCancelWater()
    {
        isWatering = false;
        GameManager.Instance.isHoldingTool = false;

        bucketAnimation.Play("anim_toolOut");
        bucketParticleSys.Play();

        yield return new WaitForSecondsRealtime(0.25f);

        WaterButton.interactable = true;
        bucket.SetActive(false);
    }
}
