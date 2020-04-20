using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0.0f, 1.0f)]
    public float volume = 1;
    [Range(0.0f, 3.0f)]
    public float pitch = 1;
    public bool playOnAwake;
    public bool loop;

    [HideInInspector]
    public AudioSource source;


    public void Init(AudioSource source)
    {
        this.source = source;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.playOnAwake = playOnAwake;
        source.loop = loop;
    }
}
