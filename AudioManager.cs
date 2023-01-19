using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    public Sound[] sounds;

    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            // first off
            s.source.enabled = false;
        }
    }

    void Start()
    {
       // Play("Walking");
    }

    
    // Walking and running sound
    public void PlayWalk(String name, bool active, bool shiftPressed)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        Sound s1 = Array.Find(sounds, sound => sound.name == "Running");
        if (active)
        {
            s.source.enabled = true;
            if (shiftPressed)
            {
                s.source.enabled = false;
                s1.source.enabled = true;
            }
            else
            {
                s1.source.enabled = false;
            }

        }
        else
        {
            s.source.enabled = false;
        }
        
    }

    //Reloading Sound
    public void PlayReload(String name, bool active)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(active)
        {
            s.source.enabled = true;
        }
        else
        {
            s.source.enabled = false;
        }
    }

    //Jumping Sound
    public void PlayJump(String name, bool active)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (active)
        {
            s.source.enabled = true;
        }
        else
        {
            s.source.enabled = false;
        }
    }
}
