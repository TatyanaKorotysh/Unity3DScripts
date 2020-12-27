using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Tasks tasks;

    public void Start()
    {
        tasks = GameObject.Find("Canvas").GetComponent<Tasks>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(tasks.Task4) MainManager.Messenger.WriteMessage("Нажмите клавишу ''Space''");
    }
}
