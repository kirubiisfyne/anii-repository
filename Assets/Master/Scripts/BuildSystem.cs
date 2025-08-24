using System;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public ObjectSelector objectSelector;
    public ObjectData objects;
    public int selectedObjectID = -1;

    public GameObject cellIndicator;
    public GameObject gridDisplay;
    public Grid grid;

    private void Start()
    {
        StopBuilding();
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
        GameObject buildngObject = Instantiate(objects.objectsData[selectedObjectID].prefab);
        buildngObject.transform.position = grid.CellToWorld(gridPosition);
    }

    private void StopBuilding()
    {
        selectedObjectID = -1;

        cellIndicator.SetActive(false);
        gridDisplay.SetActive(false);
        objectSelector.OnClickedBuild -= BuildStructure;
        objectSelector.OnExitBuild -= StopBuilding;
    }
}
