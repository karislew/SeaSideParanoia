using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Sound", menuName = "Seaside Paranoia/Sound", order = 0)]
[System.Serializable]
public class Sound : ScriptableObject
{
    //public new string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.3f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;

    public bool loop = false;
}
