using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChooseCharacter : MonoBehaviour
{
    private Text text;
    private readonly string charSelect = "charSelect";

    void Start()
    {
        text = transform.Find("CurrentPerson").GetComponent<Text>();
        name = "Кэтери";
        name = "Кэтери";
        PlayerPrefs.SetInt(charSelect, 1);
    }

    public void Choose() {
        name = EventSystem.current.currentSelectedGameObject.transform.GetChild(1).GetComponent<Text>().text;
        text.text = "Вы выбрали " + name;
        switch (name)
        {
            case "Кэтери":
                PlayerPrefs.SetInt(charSelect, 1);
                GameObject.Find("1").GetComponent<Image>().enabled = true;
                GameObject.Find("2").GetComponent<Image>().enabled = false;
                break;
            case "Глория":
                PlayerPrefs.SetInt(charSelect, 2);
                GameObject.Find("1").GetComponent<Image>().enabled = false;
                GameObject.Find("2").GetComponent<Image>().enabled = true;
                break;
        }
    }
}
