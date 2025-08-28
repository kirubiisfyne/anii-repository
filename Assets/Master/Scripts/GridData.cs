using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridData : MonoBehaviour
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new();

    public void AddObjectAt(Vector3Int gridPosition, Vector2Int objectSize, int ID, int placedObjectIndex)
    {
        List<Vector3Int> positionsToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData data = new PlacementData(positionsToOccupy, ID, placedObjectIndex);

        foreach(var position in positionsToOccupy)
        {
            if (placedObjects.ContainsKey(position)) throw new System.Exception($"Dictionary already contains this cell position {position}");

            placedObjects[position] = data;
        }
    }

    public void RemoveObjectAt(Vector3Int gridPosition)
    {
        if (!placedObjects.ContainsKey(gridPosition)) return;

        PlacementData data = placedObjects[gridPosition];

        foreach (var pos in data.occupiedPositions)
        {
            placedObjects.Remove(pos);
        }
    }


    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for (int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, 0, y));
            }
        }

        return returnVal;
    }

    public bool CanPlaceObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
        foreach (var position in positionToOccupy)
        {
            if(placedObjects.ContainsKey(position)) return false;
        }
        return true;
    }
}

public class PlacementData
{
    public List<Vector3Int> occupiedPositions;
    public int ID { get; private set; }
    public int PlacedObjectIndex { get; private set; }

    public PlacementData(List<Vector3Int> occupiedPositions, int iD, int placedObjectIndex)
    {
        this.occupiedPositions = occupiedPositions;
        ID = iD;
        PlacedObjectIndex = placedObjectIndex;
    }
}
