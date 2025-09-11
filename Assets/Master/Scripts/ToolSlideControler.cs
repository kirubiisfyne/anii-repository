using UnityEngine;

public class ToolSlideControler : MonoBehaviour
{
    public Animation anim;

    public void PlayAnim(string clipName)
    {
        if (anim != null)
        {
            anim.Play(clipName);
        }
    }
}
