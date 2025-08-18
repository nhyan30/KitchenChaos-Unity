using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance {get; private set;}
    private float volume = .4f;
    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
    }
    public void ChangeVolume()
    {
        volume += .1f;
        // volume = volume % 1.1f;

        if (volume > 1f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return volume;
    }
}
