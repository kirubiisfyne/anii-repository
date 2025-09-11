using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ToolFunction : MonoBehaviour
{
    public UnityEvent toolFunction;
    public static event Action CancelTools;
    public GameObject toolCursor;

    public void UseTool()
    {
        CancelTools?.Invoke();
        toolFunction.Invoke();
    }
}
