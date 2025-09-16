using UnityEngine;

public class ToolManager : MonoBehaviour
{
    public BuildSystem buildTool;
    public WaterSystem waterTool;
    public HarvestSystem harvestTool;
    public CosmeticsManager cosmeticsTool;

    private void OnEnable()
    {
        GameManager.Instance.ChangeTool += CancelAllTools;
    }
    public void CancelAllTools()
    {
        Debug.Log("Canceled All Tools!");

        buildTool.StopBuilding();
        waterTool.CancelWater();
        harvestTool.CancelHarvest();
    }
}
