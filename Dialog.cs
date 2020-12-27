using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialog : MonoBehaviour
{
    public string name;

    public Sprite img;

    [TextArea(3, 10)]
    public string[] sentences;

    public bool getTask = false;
}
