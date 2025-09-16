using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Settings Variables")]


    [Header("Global Variables")]
    public bool isHoldingTool = false;
    public GameObject floatingText;
    public Canvas canvas;

    public event Action ChangeTool;

    public bool IsHoldingBool
    {
        get => isHoldingTool;
        set {
            if (isHoldingTool)
            {
                isHoldingTool = value;
                ChangeTool?.Invoke();
            }
        }
    }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // prevent duplicates
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (canvas == null)
        {
            canvas = FindAnyObjectByType<Canvas>();
        }
    }
}
