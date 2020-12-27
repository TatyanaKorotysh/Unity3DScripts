using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject main, idle;

    private readonly string charSelect = "charSelect";

    public void Awake()
    {
        int getChar = PlayerPrefs.GetInt(charSelect);

        switch (getChar)
        {
            case 1:
                Instantiate(main, this.transform.position, this.transform.rotation);
                break;
            case 2:
                Instantiate(idle, this.transform.position, this.transform.rotation);
                break;
            default:
                Instantiate(main, this.transform.position, this.transform.rotation);
                break;
        }
    }
}
