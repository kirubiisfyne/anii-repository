using UnityEngine;

public class CellIndicator : MonoBehaviour
{
    public Grid grid;
    public ObjectSelector objectSelector;

    private void Update()
    {
        Vector3 mousePosition = objectSelector.GetSelectedMousePosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        transform.position = new Vector3(grid.CellToWorld(gridPosition).x, 0.05f, grid.CellToWorld(gridPosition).z);
    }
}
