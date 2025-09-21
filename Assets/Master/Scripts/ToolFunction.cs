using System;
using UnityEngine;
using UnityEngine.Events;

public class ToolFunction : MonoBehaviour
{
    public UnityEvent toolFunction;
    public static event Action CancelTools;
    public static event Action CancelPlants;
    public GameObject toolCursor;

    public void UseTool()
    {
        CancelTools?.Invoke();
        toolFunction.Invoke();
    }

    public void CancelTool()
    {
        CancelTools?.Invoke();
    }

    public void CancelPlant()
    {
        CancelPlants?.Invoke();
    }
}
