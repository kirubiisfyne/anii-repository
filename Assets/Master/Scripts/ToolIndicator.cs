using UnityEngine;
using UnityEngine.InputSystem;

public class ToolIndicator : MonoBehaviour
{
    public Transform tool;
    public LayerMask groundLayer;

    public float cursorYOffset;
    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            Vector3 targetPos = hit.point;
            targetPos.y = cursorYOffset;
            tool.position = targetPos;
        }
    }
}
