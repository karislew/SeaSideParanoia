using System;
using UnityEngine.Audio;
using UnityEngine;


public class AudioManager : Singleton<AudioManager>
{
    public string path = "Audio";
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public Sound[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        //source = GetComponent<AudioSource>();
        sounds = Resources.LoadAll<Sound>(path);

        // TODO: add main theme to play on game start
        Play("MainTheme");
    }

    public void Play(string name)
    {
        AudioSource source;

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound: \"" + name + "\" not found!");
            return;
        }

        switch (s.type)
        {
            case SoundType.SFX:
                source = sfxSource;
                break;
            case SoundType.BGM:
                source = bgmSource;
                break;
            default:
                source = sfxSource;
                break;
        }

        source.clip = s.clip;
        source.volume = s.volume;
        source.pitch = s.pitch;
        source.loop = s.loop;

        source.Play();
        Debug.Log("playing " + s.name);
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }
    public void StopSFX()
    {
        sfxSource.Stop();
    }
    public void StopAll()
    {
        StopBGM();
        StopSFX();
    }
}