using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    private static AudioManager instance;

    public static AudioManager GetInstance()
    {
        return instance;
    }

    public void Awake()
    {
        AudioManager.instance = this;
    }

    public AudioSource source;
    public AudioClip[] clips;

    public void PlaySound(string name)
    {
        if (name == "click")
            PlaySound(0);
        else if (name == "drop")
            PlaySound(1);
    }

    public void PlaySound(int i)
    {
        source.clip = clips[i];
        source.Play();
    }

    public void PlaySound(AudioClip a)
    {
        source.clip = a;
        source.Play();
    }
	
}
