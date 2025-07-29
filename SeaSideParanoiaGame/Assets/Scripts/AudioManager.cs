using System;
using UnityEngine.Audio;
using UnityEngine;


public class AudioManager : Singleton<AudioManager>
{
    public string path = "Audio";
    public AudioSource source;
    public Sound[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();
        sounds = Resources.LoadAll<Sound>(path);

        // TODO: add main theme to play on game start
        Play("FakeMainTheme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound: \"" + name + "\" not found!");
            return;
        }

        source.clip = s.clip;
        source.volume = s.volume;
        source.pitch = s.pitch;
        source.loop = s.loop;

        source.Play();
    }
}