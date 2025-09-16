using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectBuildInfo : MonoBehaviour
{
    public TMP_Text TMPName;
    public TMP_Text TMPCost;

    public Animation buildInfoAnimation;

    public Image rootImage;

    public void HideBuildInfo()
    {
        StartCoroutine(StartHideBuildInfo());
    }
    private IEnumerator StartHideBuildInfo()
    {
        buildInfoAnimation.Play("anim_UIPannelOut");
        yield return new WaitForSecondsRealtime(0.25f);
        rootImage.gameObject.SetActive(false);
    }
}
