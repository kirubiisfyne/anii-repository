using UnityEngine;

public class CosmeticsManager : MonoBehaviour
{
    public Animation cosmeticAnimation;
    public AudioSource sfx;
    public ParticleSystem particle;

    public void OnBuildCosmetic()
    {
        particle.Play();
        cosmeticAnimation.Play("anim_build");
        sfx.Play();

        Debug.Log("Played Cosmetic Build Animation!");
    }

    public void OnRemoveCosmetic()
    {
        particle.Play();
        cosmeticAnimation.Play("anim_remove");
        sfx.Play();

        Destroy(transform.parent.gameObject, 0.9f);
    }
}
