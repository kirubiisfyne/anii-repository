using UnityEngine;

public class Soil : MonoBehaviour
{
    public SoilType soilType;

    public Transform[] plantingSlots; // assign 2 slots in inspector
    private bool[] occupied;

    void Awake()
    {
        occupied = new bool[plantingSlots.Length];
    }

    // Try to plant crop in the nearest free slot
    public bool TryPlant(GameObject cropPrefab, Vector3 hitPoint)
    {
        int nearestIndex = -1;
        float nearestDist = Mathf.Infinity;

        for (int i = 0; i < plantingSlots.Length; i++)
        {
            if (occupied[i]) continue; // skip used slot

            float dist = Vector3.Distance(hitPoint, plantingSlots[i].position);
            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearestIndex = i;
            }
        }

        if (nearestIndex != -1)
        {
            Instantiate(cropPrefab, plantingSlots[nearestIndex].position, Quaternion.Euler(-90, 0, 0));
            occupied[nearestIndex] = true;
            return true;
        }

        return false; // no free slot
    }
}
