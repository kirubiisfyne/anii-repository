using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CropLock : MonoBehaviour
{
    public List<Button> cropButtons = new List<Button> { };
    private void OnEnable()
    {
        GameManager.Instance.OnLevelUp += HandleLevelUp;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelUp -= HandleLevelUp;
    }

    private void HandleLevelUp(int level)
    {
        Debug.Log("Player reached level " + level);

        for (int i = 0; i < cropButtons.Count; i++)
        {
            Debug.Log("CropLock.cs: " + i + " " + GameManager.Instance.cropKeys[i].ToString());
            cropButtons[i].interactable = GameManager.Instance.cropKeys[i];
            Debug.Log("Is cropButton interactable?: " + cropButtons[i].interactable);
        }
    }
}
