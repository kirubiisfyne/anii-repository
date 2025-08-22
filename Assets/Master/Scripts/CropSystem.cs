using UnityEngine;

public class CropSystem : MonoBehaviour
{
    [Header("Growth Settings")]
    public GameObject[] growthStages; // assign prefabs/meshes for each stage
    public float growthInterval = 30f; // time between stages (in seconds)

    private int currentStage = 0;
    private GameObject currentModel;

    public bool IsMature => currentStage >= growthStages.Length - 1;

    private Soil soil;       // reference to the soil it was planted on
    private int slotIndex;   // which slot in the soil

    void Start()
    {
        // Start at stage 0
        if (growthStages.Length > 0)
            SetStage(0);

        // Start growing
        InvokeRepeating(nameof(Grow), growthInterval, growthInterval);
    }

    void SetStage(int stageIndex)
    {
        // Destroy old model
        if (currentModel != null)
            Destroy(currentModel);

        // Spawn new model as child
        currentModel = Instantiate(growthStages[stageIndex], transform.position, Quaternion.identity, transform);
    }

    void Grow()
    {
        if (currentStage < growthStages.Length - 1)
        {
            currentStage++;
            SetStage(currentStage);
        }
        else
        {
            // Already fully grown → stop growing
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
        // free up slot in soil
        if (soil != null)
        {
            soil.ClearSlot(slotIndex);
        }

        Destroy(gameObject);
    }
}

