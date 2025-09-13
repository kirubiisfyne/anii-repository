using UnityEngine;

[CreateAssetMenu(menuName = "Farming/Crop Data")]
public class CropData : ScriptableObject
{
    public string cropName;
    public Sprite cursorSprite;     // UI image for cursor.
    public GameObject cropPrefab;   // prefab to spawn in world.
    public SoilType[] allowedSoils; // soil types this crop can grow in.

    [TextArea(3, 10)] public string cropDesciption;
}
