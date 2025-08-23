using UnityEngine;

public class Soil : MonoBehaviour
{
    public SoilType soilType;
    public Transform[] plantingSlots;
    private GameObject[] occupiedSlots;

    void Awake()
    {
        occupiedSlots = new GameObject[plantingSlots.Length];
    }

    public bool TryPlant(GameObject cropPrefab, int slotIndex)
    {
        if (occupiedSlots[slotIndex] != null) return false;

        // Instantiate as child of the slot.
        GameObject crop = Instantiate(
            cropPrefab,
            plantingSlots[slotIndex].position,
            Quaternion.identity,
            plantingSlots[slotIndex]   // Parent is now the slot.
        );

        occupiedSlots[slotIndex] = crop;

        // Initialize crop with soil reference.
        crop.GetComponent<CropSystem>().Init(this, slotIndex);

        return true;
    }

    public void ClearSlot(int index)
    {
        occupiedSlots[index] = null;
    }

    public int GetClosestSlot(Vector3 hitPoint)
    {
        int closestIndex = 0;
        float closestDist = float.MaxValue;

        for (int i = 0; i < plantingSlots.Length; i++)
        {
            float dist = Vector3.Distance(hitPoint, plantingSlots[i].position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestIndex = i;
            }
        }

        return closestIndex;
    }

}
