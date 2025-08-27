using UnityEngine;

public class PitchShifter : MonoBehaviour
{
    public AudioSource audioSource;
    public Vector2 pitchRange;

    private void Start()
    {
        audioSource.pitch = Random.Range(pitchRange.x, pitchRange.y);
    }
}
