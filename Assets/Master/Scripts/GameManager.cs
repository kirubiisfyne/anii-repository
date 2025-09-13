using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isHoldingTool = false;
    public GameObject floatingText;
    public Canvas canvas;

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
}
