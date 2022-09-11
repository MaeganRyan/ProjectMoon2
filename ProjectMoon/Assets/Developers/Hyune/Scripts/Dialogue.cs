using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// have a way to break out of the cameraMovement.
// use yield break;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    [SerializeField] public List<Line> voiceLines;
}

[System.Serializable]
public class Line
{
    public Speaker speaker;
    public string line = "";
}
