using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CropLock : MonoBehaviour
{
    public List<Button> cropButtons = new List<Button> { };
    private void OnEnable()
    {
        PointsEXPSystem.Instance.OnLevelUp += HandleLevelUp;
    }

    private void OnDisable()
    {
        PointsEXPSystem.Instance.OnLevelUp -= HandleLevelUp;
    }

    private void HandleLevelUp(int level)
    {
        Debug.Log("Player reached level " + level);

        for (int i = 0; i < cropButtons.Count; i++)
        {
            Debug.Log("CropLock.cs: " + i + " " + PointsEXPSystem.Instance.cropKeys[i].ToString());
            cropButtons[i].interactable = PointsEXPSystem.Instance.cropKeys[i];
            Debug.Log("Is cropButton interactable?: " + cropButtons[i].interactable);
        }
    }
}
