using System.Collections.Generic;
using System;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    [Header("Object Data")]
    public ObjectSelector objectSelector;
    public ObjectList objects;
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
        objectSelector.OnObjectRemoved += HandleRemoveObject;
    }

    public void StartBuild(int ID)
    {
        StopBuilding();

        selectedObjectID = objects.objectsData.FindIndex(x => x.objectInstance.ID == ID);

        if (selectedObjectID < 0)
        {
            Debug.LogError($"No ID found {ID}");
            return;
        }

        previewSystem.StartShowingPlacementPreview(objects.objectsData[selectedObjectID].objectInstance.Prefab, objects.objectsData[selectedObjectID].objectInstance.Size);
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

        GameObject buildngObject = Instantiate(objects.objectsData[selectedObjectID].objectInstance.Prefab);
        buildngObject.transform.position = grid.CellToWorld(gridPosition);

        buildngObject.GetComponentInChildren<Soil>().OnBuildSoil();

        placedObjects.Add(buildngObject);

        objectData.AddObjectAt(gridPosition, objects.objectsData[selectedObjectID].objectInstance.Size, objects.objectsData[selectedObjectID].objectInstance.ID, placedObjects.Count - 1);

        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);

        StopBuilding();
    }

    private void HandleRemoveObject(GameObject obj)
    {
        Vector3Int gridPosition = grid.WorldToCell(obj.transform.position);

        objectData.RemoveObjectAt(gridPosition);

        int index = placedObjects.IndexOf(obj);
        if (index >= 0)
        {
            placedObjects[index] = null;
        }
    }

    private bool CheckPlacementVadility(Vector3Int gridPosition, int selectedObjectID)
    {
        return objectData.CanPlaceObjectAt(gridPosition, objects.objectsData[selectedObjectID].objectInstance.Size);
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
