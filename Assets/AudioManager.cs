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

    public AudioSource[] sources;
    public int currentSource = 0;

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
        sources[currentSource].clip = clips[i];
        sources[currentSource].Play();
        currentSource = (currentSource + 1) % sources.Length;
    }

    public void PlaySound(AudioClip a)
    {
        sources[currentSource].clip = a;
        sources[currentSource].Play();

        currentSource = (currentSource + 1) % sources.Length;
    }
	
}
