using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public bool isBuilding;

    //Soil Picker
    [Range(0, 2)] public int currentSoilIndex = 0;
    private InputAction scrollAction;

    private void OnEnable()
    {
        ToolFunction.CancelTools += StopBuilding;

        if (scrollAction == null)
        {
            scrollAction = new InputAction(type: InputActionType.Value, binding: "<Mouse>/scroll");
        }
        scrollAction.Enable();
    }

    private void OnDisable()
    {
        ToolFunction.CancelTools -= StopBuilding;

        scrollAction.Disable();
    }
    private void Start()
    {
        StopBuilding();

        objectData = new();
        objectSelector.OnObjectRemoved += HandleRemoveObject;
    }

    public void StartBuild()
    {
        int ID = currentSoilIndex;

        StopBuilding();

        isBuilding = true;
        GameManager.Instance.isHoldingTool = true;

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
        isBuilding = false;
        GameManager.Instance.isHoldingTool = false;

        selectedObjectID = -1;

        previewSystem.StopShowingPreview();
        gridDisplay.SetActive(false);
        objectSelector.OnClickedBuild -= BuildStructure;
        objectSelector.OnExitBuild -= StopBuilding;
    }

    private void HandleScrollChange()
    {
        Vector2 scoll = scrollAction.ReadValue<Vector2>();
        int oldIndex = currentSoilIndex;

        if (scoll.y > 0f)
        {
            currentSoilIndex++;
            if (currentSoilIndex > 2) currentSoilIndex = 0;
        }
        else if (scoll.y < 0f)
        {
            currentSoilIndex--;
            if (currentSoilIndex < 0) currentSoilIndex = 2;
        }

        if (currentSoilIndex != oldIndex)
        {
            StartBuild();

            Vector3 mousePosition = objectSelector.GetSelectedMousePosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            bool placementValidity = CheckPlacementVadility(gridPosition, selectedObjectID);
            previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);

            lastDetectedPosition = gridPosition;
        }
    }

    private void Update()
    {
        if (selectedObjectID < 0) return;

        HandleScrollChange();

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
