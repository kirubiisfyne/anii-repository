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
    public GameObject gridDisplay;
    public Grid grid;

    public PreviewSystem previewSystem;

    private GridData objectData;

    private List<GameObject> placedObjects = new();

    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    private void Start()
    {
        StopBuilding();

        objectData = new();
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

        previewSystem.StartShowingPlacementPreview(objects.objectsData[selectedObjectID].prefab, objects.objectsData[selectedObjectID].size);
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

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
    }

    private bool CheckPlacementVadility(Vector3Int gridPosition, int selectedObjectID)
    {
        return objectData.CanPlaceObjectAt(gridPosition, objects.objectsData[selectedObjectID].size);
    }

    private void StopBuilding()
    {
        selectedObjectID = -1;

        previewSystem.StopShowingPreview();
        gridDisplay.SetActive(false);
        objectSelector.OnClickedBuild -= BuildStructure;
        objectSelector.OnExitBuild -= StopBuilding;
    }

    private void Update()
    {
        if (selectedObjectID < 0) return;

        Vector3 mousePosition = objectSelector.GetSelectedMousePosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        if(lastDetectedPosition != gridPosition)
        {
            bool placementVadility = CheckPlacementVadility(gridPosition, selectedObjectID);

            previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementVadility);
            lastDetectedPosition = gridPosition;
        }
    }
}
