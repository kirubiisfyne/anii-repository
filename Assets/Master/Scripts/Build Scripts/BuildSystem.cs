using System;
using System.Collections.Generic;
using TMPro;
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
    [Range(3, 5)] public int currentCosmeticIndex = 3;

    private int[] currentIndexRange = new int[] { };
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

    public void BuildCosmetics()
    {
        currentIndexRange = new int[] { 3, 5 };
        currentSoilIndex = 3;
    }
    public void BuildSoil()
    {
        currentIndexRange = new int[] { 0, 2 };
        currentSoilIndex = 0;

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

        if (PointsEXPSystem.Instance.CurrentPoints < objects.objectsData[selectedObjectID].objectInstance.buildCost)
        {
            GameObject floatingText = Instantiate(GameManager.Instance.floatingText, GameManager.Instance.canvas.transform);
            floatingText.GetComponent<TMP_Text>().text = "Not Enough Coins!";
            return;
            
        }

        // Deduct points upon building.
        PointsEXPSystem.Instance.DecreasePoints(objects.objectsData[selectedObjectID].objectInstance.buildCost);

        Vector3 mousePosition = objectSelector.GetSelectedMousePosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        bool placementVadility = CheckPlacementVadility(gridPosition, selectedObjectID);
        if (!placementVadility) return;

        GameObject buildngObject = Instantiate(objects.objectsData[selectedObjectID].objectInstance.Prefab);
        buildngObject.transform.position = grid.CellToWorld(gridPosition);

        if (buildngObject.GetComponentInChildren<Soil>() != null)
        {
            buildngObject.GetComponentInChildren<Soil>().OnBuildSoil();
        }
        else if (buildngObject.GetComponentInChildren<CosmeticsManager>() != null)
        {
            buildngObject.GetComponentInChildren<CosmeticsManager>().OnBuildCosmetic();
        }

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

    public void StopBuilding()
    {
        isBuilding = false;
        GameManager.Instance.isHoldingTool = false;

        selectedObjectID = -1;

        previewSystem.StopShowingPreview();
        gridDisplay.SetActive(false);
        objectSelector.OnClickedBuild -= BuildStructure;
        objectSelector.OnExitBuild -= StopBuilding;
    }

    private void HandleScrollChange(int[] indexRange)
    {
        Vector2 scoll = scrollAction.ReadValue<Vector2>();
        int oldIndex = currentSoilIndex;

        if (scoll.y > 0f)
        {
            currentSoilIndex++;
            if (currentSoilIndex > indexRange[1]) currentSoilIndex = indexRange[0];
        }
        else if (scoll.y < 0f)
        {
            currentSoilIndex--;
            if (currentSoilIndex < indexRange[0]) currentSoilIndex = indexRange[1];
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

        HandleScrollChange(currentIndexRange);

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
