using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound not found: " + name);
            return;
        }

        if (!s.source.isPlaying)
            s.source.Play();
    }

    public void PlayLoop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) return;

        if (!s.source.isPlaying)
        {
            s.source.loop = true;
            s.source.Play();
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) return;

        s.source.Stop();
    }
}