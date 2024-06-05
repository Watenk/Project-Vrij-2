using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Clip")]
public class AudioElement : ScriptableObject
{
    public int id;
    public AudioClip clip;
}
