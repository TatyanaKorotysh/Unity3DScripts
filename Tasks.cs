using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Tasks : MonoBehaviour
{
    public bool Task1 = true;
    public bool Task2 = true;
    public bool Task3 = true;
    public bool Task4 = false;
    public bool Inventary = false;

    public AudioSource yes;
    public AudioSource no;

    TooltipTextUI tooltip;

    [SerializeField]
    private GameObject Task;

    [SerializeField]
    GameObject taskWindow;

    Text task; 

    private InventoryManager inv;

    private void Start()
    {
        Task.SetActive(true);
        task = GameObject.Find("Task").transform.GetChild(0).GetComponent<Text>();
        tooltip = FindObjectOfType<TooltipTextUI>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Task.SetActive(!Task.activeSelf); 
        }
    }

    public void WriteTask(string text) 
    {
        this.task.text = text;
    }

    public void TaskTrue()
    {
        yes.Play();

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            inv = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        }

        taskWindow.SetActive(false);

        if (Task1 == false)
        {
            Task1 = true;
            MainManager.Task.WriteTask("Найди лесника");
            MainManager.Messenger.WriteMessage("Найди лесника");
            tooltip.text = "Пройди через портал, чтобы найти лесника";
        }
        else if (Task2 == false) {
            Task2 = true;
            inv.Inventory.SetActive(true);
            MainManager.Task.WriteTask("Собери растения");
            MainManager.Messenger.WriteMessage("Собери растения");
            tooltip.text = "Нажми клавишу ''I'' чтобы открыть инвентарь. Тебе нужно собрать по 5 единиц каждого вида растений растений";
        }
        else if (Task3 == false)
        {
            Task3 = true;
            MainManager.Task.WriteTask("Верни кулон");
            MainManager.Messenger.WriteMessage("Верни кулон");
            tooltip.text = "Теперь у тебя есть кулон! Сейчас ты сможешь вернуть его и узнать дорогу из леса";
        }
        else if (Task4 == false)
        {
            Task4 = true;
            MainManager.Task.WriteTask("Выйди из леса");
            MainManager.Messenger.WriteMessage("Выйди из леса");
            tooltip.text = "Чтобы выйти из леса, вернись к домику лесника. Выход прямо за дверью!";
        }
    }

    public void TaskFalse()
    {
        no.Play();

        taskWindow.SetActive(false);
        if (Task1 == false)
        {
            Task1 = false;
            MainManager.Task.WriteTask("Найди кого-нибудь, чтобы спросить дорогу");
            MainManager.Messenger.WriteMessage("Найди кого-нибудь чтобы спросить дорогу");
            tooltip.text = "Поброди по лесу и найди кого-нибудь, чтобы спросить дорогу";
        }
        else if (Task2 == false)
        {
            Task2 = false;
            MainManager.Task.WriteTask("Найди лесника");
            MainManager.Messenger.WriteMessage("Найди лесника");
            tooltip.text = "Пройди через портал, чтобы найти лесника";
        }
        else if (Task3 == false)
        {
            Task3 = false;
            MainManager.Task.WriteTask("Собери растения");
            MainManager.Messenger.WriteMessage("Собери растения");
            tooltip.text = "Нажми клавишу ''I'' чтобы открыть инвентарь. Тебе нужно собрать по 5 единиц каждого вида растений растений";
        }
        else if (Task4 == false)
        {
            Task4 = false;
            MainManager.Task.WriteTask("Верни кулон");
            MainManager.Messenger.WriteMessage("Верни кулон");
            tooltip.text = "Теперь у тебя есть кулон! Сейчас ты сможешь вернуть его и узнать дорогу из леса";
        }
    }
}
