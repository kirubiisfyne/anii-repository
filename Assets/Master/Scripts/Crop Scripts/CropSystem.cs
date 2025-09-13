using UnityEngine;

public class CropSystem : MonoBehaviour
{
    [Header("Crop Info")]
    public CropData cropData;

    [Header("Growth Settings")]
    public GameObject[] growthStages; // assign prefabs/meshes for each stage.
    public float growthInterval = 30f; // time between stages (in seconds).
    public int waterRequierement = 1;

    [Header("Harvest Settings")]
    public bool wasWatered = false;
    public bool hasWilted = false;
    public int growPoints = 10;
    public int currentWater = 0;

    private int currentStage = 1;
    private GameObject currentModel;

    public bool IsMature => currentStage >= growthStages.Length - 1;

    [Header("Particle Systems")]
    public ParticleSystem dustCloud;
    public ParticleSystem fallingLeaves;
    public ParticleSystem waterSplash;
    public Animation cropAnimation;

    private Soil soil;       // reference to the soil it was planted on.
    private int slotIndex;   // which slot in the soil.

    void Start()
    {
        dustCloud.Play();
        cropAnimation.Play("anim_cropSpawn");

        // Start at stage 1.
        if (growthStages.Length > 1)
            SetStage(1);

        // Start growing.
        InvokeRepeating(nameof(Grow), growthInterval, growthInterval);
    }

    void SetStage(int stageIndex)
    {
        // Destroy old model.
        if (currentModel != null)
            Destroy(currentModel);

        // Spawn new model as child.
        currentModel = Instantiate(growthStages[stageIndex], transform.position, Quaternion.identity, transform);

        // Reset watered state.
        currentWater = 0;
        wasWatered = false;
    }

    void Grow()
    {
        if (currentStage < growthStages.Length - 1)
        {
            if (currentWater >= waterRequierement)
            {
                currentStage++;
                wasWatered = true;
            }
            else
            {
                currentStage = 0;
                hasWilted = true;

                CancelInvoke(nameof(Grow));
            }

            SetStage(currentStage);
        }
        else
        {
            // Already fully grown → stop growing.
            CancelInvoke(nameof(Grow));
        }
    }
    public void Init(Soil soilRef, int index)
    {
        soil = soilRef;
        slotIndex = index;
    }

    public void Harvest()
    {
        // Free up slot in soil.
        if (soil != null)
        {
            soil.ClearSlot(slotIndex);
        }

        // Award points only if crop is mature and not wilted
    if (PointsEXPSystem.Instance != null)
    {
        if (!hasWilted && IsMature)
        {
            PointsEXPSystem.Instance.AddPoints(growPoints);
        }
        else if (hasWilted)
        {
            Debug.Log("Wilted crop harvested — no points awarded.");
        }
    }

        cropAnimation.Play("anim_cropHarvest");
        fallingLeaves.Play();
        Destroy(gameObject, 0.9f);
    }

    public void Water()
    {
        if (currentWater < waterRequierement)
        {
            currentWater++;

            waterSplash.Play();
            cropAnimation.Play("anim_cropWater");
            Debug.Log($"Crop Water: {currentWater}, and has Wilted: {hasWilted}");
        }
        else wasWatered = true;
    }
}

