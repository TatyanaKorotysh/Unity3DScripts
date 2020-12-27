using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    public GameObject Inventory; 

    [SerializeField]
    UIObject[] objects; //массив элементов UI, отображающих предметы

    public GameObject[] progress;
    private Tasks tasks;

    private UIObject current = null;

    private void Start()
    {
        Inventory.SetActive(false); 
        tasks = GameObject.Find("Canvas").GetComponent<Tasks>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            if (tasks.Task2 && !tasks.Task3)
                Inventory.SetActive(!Inventory.activeSelf); // инвертируем состояние инвентаря
        }
    }
    
    public void AddItem(GameObject objectInScene) {
        foreach (UIObject obj in objects) 
        {
            if (objectInScene.name == obj.name)
            {
                obj.State = true; 
                current = obj;
                break; 
            }
        }

        UpdateUI();

        foreach (GameObject obj in progress) 
        {
            if (obj.GetComponent<Image>().fillAmount == 1)
            {
                tasks.Inventary = true;
            }
            else
            {
                tasks.Inventary = false;
                break;
            }
        }

        if (tasks.Inventary)
        {
            Inventory.SetActive(false);
            MainManager.Messenger.WriteMessage("Вы собрали все растения");
        }
    }

    void UpdateUI() // метод обновления инвентаря
    {
            current.UpdateImage(); 
    }
}
