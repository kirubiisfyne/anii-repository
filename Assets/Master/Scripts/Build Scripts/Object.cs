using UnityEngine;

[System.Serializable]
public class Object
{
    public string Name;
    public int ID;
    public int buildCost;
    public GameObject Prefab;
    public Vector2Int Size = Vector2Int.one;

    [TextArea(3, 10)] public string objectDescription;
}
