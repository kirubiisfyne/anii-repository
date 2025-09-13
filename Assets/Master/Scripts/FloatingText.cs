using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
