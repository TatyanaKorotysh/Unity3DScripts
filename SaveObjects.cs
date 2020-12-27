using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObjects : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
