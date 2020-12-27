using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Messenger : MonoBehaviour
{
    Text message; 
    static Coroutine RunMessage; 

    private void Start()
    {
        message = GetComponent<Text>();
        transform.parent.gameObject.GetComponent<Image>().enabled = false;
    }

    public void WriteMessage(string text) // метод для запуска корутины с выводом сообщения
    {
        transform.parent.gameObject.GetComponent<Image>().enabled =  true;
        if (RunMessage != null) {
            StopCoroutine(RunMessage);
        }
        this.message.text = "";
        RunMessage = StartCoroutine(Message(text));
    }

    IEnumerator Message(string message) // корутина для вывода сообщений
    {
        this.message.text = message;
        yield return new WaitForSeconds(4f); 
        this.message.text = ""; 
        transform.parent.gameObject.GetComponent<Image>().enabled = false;
    }

    IEnumerator MessageOff() // корутина для вывода сообщений
    {
        yield return new WaitForSeconds(4f);
        this.message.text = "";
        transform.parent.gameObject.GetComponent<Image>().enabled = false;
    }
}
