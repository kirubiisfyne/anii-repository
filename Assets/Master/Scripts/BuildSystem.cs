using System.Collections.Generic;
using System;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [Header("Object Data")]
    public ObjectSelector objectSelector;
    public ObjectData objects;
    public int selectedObjectID = -1;

    [Header("Grid")]
    public GameObject cellIndicator;
    public GameObject gridDisplay;
    public Grid grid;

    private GridData objectData;

    private Renderer previewRenderer;

    private List<GameObject> placedObjects = new();

    private void Start()
    {
        StopBuilding();

        objectData = new();
        previewRenderer = cellIndicator.GetComponentInChildren<Renderer>();
    }

    public void StartBuild(int ID)
    {
        StopBuilding();

        selectedObjectID = objects.objectsData.FindIndex(x => x.ID == ID);

        if (selectedObjectID < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        cellIndicator.SetActive(true);
        gridDisplay.SetActive(true);
        objectSelector.OnClickedBuild += BuildStructure;
        objectSelector.OnExitBuild += StopBuilding;
    }

    private void BuildStructure()
    {
        if (objectSelector.IsPointerOverUI())
        {
            return;
        }

        Vector3 mousePosition = objectSelector.GetSelectedMousePosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementVadility = CheckPlacementVadility(gridPosition, selectedObjectID);
        if (!placementVadility) return;

        GameObject buildngObject = Instantiate(objects.objectsData[selectedObjectID].prefab);
        buildngObject.transform.position = grid.CellToWorld(gridPosition);
        placedObjects.Add(buildngObject);

        objectData.AddObjectAt(gridPosition, objects.objectsData[selectedObjectID].size, objects.objectsData[selectedObjectID].ID, placedObjects.Count - 1);
    }

    private bool CheckPlacementVadility(Vector3Int gridPosition, int selectedObjectID)
    {
        return objectData.CanPlaceObjectAt(gridPosition, objects.objectsData[selectedObjectID].size);
    }

    private void StopBuilding()
    {
        selectedObjectID = -1;

        cellIndicator.SetActive(false);
        gridDisplay.SetActive(false);
        objectSelector.OnClickedBuild -= BuildStructure;
        objectSelector.OnExitBuild -= StopBuilding;
    }

    private void Update()
    {
        if (selectedObjectID < 0) return;

        Vector3 mousePosition = objectSelector.GetSelectedMousePosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementVadility = CheckPlacementVadility(gridPosition, selectedObjectID);
        previewRenderer.material.color = placementVadility ? Color.white : Color.red;
    }
}
