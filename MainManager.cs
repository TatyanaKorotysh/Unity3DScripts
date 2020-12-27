using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    static Messenger messenger;
    static Tasks task;
    public static SceneChanger sceneChanger;
    static InventoryManager inventory;
    public static GameManager game;

    public static Messenger Messenger
    {
        get
        {
            if (messenger == null) // инициализация по запросу
            { messenger = FindObjectOfType<Messenger>(); }
            return messenger;
        }
        private set { messenger = value; }
    }

    public static Tasks Task
    {
        get
        {
            if (task == null)
            {
                task = FindObjectOfType<Tasks>();
            }
            return task;
        }
        private set
        {
            task = value;
        }
    }

    public static InventoryManager Inventory
    {
        get
        {
            if (inventory == null)
            {
                inventory = FindObjectOfType<InventoryManager>();
            }
            return inventory;
        }
        private set
        {
            inventory = value;
        }
    }

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
        sceneChanger = GetComponent<SceneChanger>();
        game = GetComponent<GameManager>();
    }
}
