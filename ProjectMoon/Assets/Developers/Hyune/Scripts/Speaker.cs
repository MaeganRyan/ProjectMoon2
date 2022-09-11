using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Speaker : ScriptableObject
{
    public string _name;
    public List<AudioClip> voiceLines = new List<AudioClip>(3);
}
