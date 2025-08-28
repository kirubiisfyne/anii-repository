
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    [SerializeField]
    private float previewYOffset = 0.06f;

    [SerializeField]
    private GameObject previewObject;

    [SerializeField]
    private Material previewMaterialPrefab;
    private Material previewMaterialInstance;

    private void Start()
    {
        previewMaterialInstance = new Material(previewMaterialPrefab);
    }

    public void StartShowingPlacementPreview(GameObject prefab, Vector2Int size)
    {
        previewObject = Instantiate(prefab);
        PreparePreview(previewObject);
    }


    private void PreparePreview(GameObject previewObject)
    {
        MeshCollider collider = previewObject.GetComponentInChildren<MeshCollider>();
        Renderer renderer = previewObject.GetComponentInChildren<Renderer>();
        Material[] materials = renderer.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = previewMaterialInstance;
        }
        renderer.materials = materials;
        collider.enabled = false;
    }

    public void StopShowingPreview()
    {
        if (previewObject != null)
            Destroy(previewObject);
    }

    public void UpdatePosition(Vector3 position, bool validity)
    {
        if (previewObject != null)
        {
            MovePreview(position);
            ApplyFeedbackToPreview(validity);

        }
    }

    private void ApplyFeedbackToPreview(bool validity)
    {
        Color c = validity ? Color.white : Color.red;

        c.a = 0.5f;
        previewMaterialInstance.color = c;
    }


    private void MovePreview(Vector3 position)
    {
        previewObject.transform.position = new Vector3(
            position.x,
            position.y + previewYOffset,
            position.z);
    }
}