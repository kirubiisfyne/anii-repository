using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class WaterSystem : MonoBehaviour
{
    [Header("UI References")]
    public Image waterCursor;   // UI image for water tool
    public AudioSource audioSource;

    [Header("Object Reference")]
    public GameObject bucket;
    public Animation bucketAnimation;
    public ParticleSystem bucketParticleSys;

    private bool isWatering = false;

    private void OnEnable()
    {
        ToolFunction.CancelTools += CancelWater;
    }

    private void OnDisable()
    {
        ToolFunction.CancelTools -= CancelWater;
    }

    void Start()
    {
        waterCursor.enabled = false;
    }

    void Update()
    {
        if (isWatering)
        {
            // Follow mouse.
            Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
            mouseScreenPos.z = 10f; // distance from camera (adjust based on your setup)

            Vector3 worldPos = Camera.main.ViewportToWorldPoint(mouseScreenPos);
            worldPos.y = 1f; // lock Y height in world space

            bucket.transform.position = worldPos;

            // Left click = try water.
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    CropSystem crop = hit.collider.GetComponentInParent<CropSystem>();
                    if (crop != null)
                    {
                        if (crop.hasWilted) return;

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

        bucketAnimation.Play("anim_toolOut");
        bucketParticleSys.Play();
        yield return new WaitForSecondsRealtime(0.25f);
        bucket.SetActive(false);
    }
}
