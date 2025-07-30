using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    BGM,
    SFX,
}


[CreateAssetMenu(fileName = "Sound", menuName = "Seaside Paranoia/Sound", order = 3)]
[System.Serializable]
public class Sound : ScriptableObject
{
    //public new string name;
    public SoundType type = SoundType.SFX;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.5f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;

    public bool loop = false;
}
