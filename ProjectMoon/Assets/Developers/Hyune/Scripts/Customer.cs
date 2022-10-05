using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Customers
{
    Robot = 0,
    Gru,
    WingedDude,
    Joker,
    Gnome,
    Scientist,
    Pumpkin,
    Grass,
    FatDude
}

[CreateAssetMenu]
public class Customer : ScriptableObject
{
    public Customers _name;
    public Sprite sprite;
    public List<AudioClip> voiceLines = new List<AudioClip>(3);

    public string needLine;
    public string thankLine;
    public string wrongLine;
    public string angerLine;
}
