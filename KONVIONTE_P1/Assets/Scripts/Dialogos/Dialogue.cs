using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta clase esta para escribir los dialogos que queramos desde el editor

[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3, 10)] public string[] sentences;
}
