using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Hand : MonoBehaviour
{
    Transform interactObject; // объект для взаимодействия
    Transform inHand;
    
    private Tasks tasks;

    [SerializeField]
    IKAnimation playerIK; // ссылка на экземпляр скрипта IKAnimation
    
    GameObject taskWindow;

    private void Start()
    {
        tasks = GameObject.Find("Canvas").GetComponent<Tasks>();
        taskWindow = GameObject.Find("TaskAgree");
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("item") || other.CompareTag("itemForTransfer"))
        {
            interactObject = other.transform; // записываем объект для взаимодействия
            playerIK.StartInteraction(other.gameObject.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("item") || other.CompareTag("itemForTransfer"))
        {
            MainManager.Messenger.WriteMessage("Нажмите клавишу ''F'' чтобы подобрать предмет");
        }
    }

//-------------БРОСИТЬ ОБЪЕКТ--------------------
    private void FixedUpdate()
    {
        CheckDistance(); // проверка дистанции с объектом

        if (Input.GetKeyDown(KeyCode.R))
        {
            ThroughItem();
        }
    }

    void CheckDistance() // метод для проверки дистанции, чтобы была возможность прекратить взаимодействие с объектом при отдалении
    {
        if (interactObject != null && Vector3.Distance(transform.position, interactObject.position) > 5)
        {
            interactObject = null;
            playerIK.StopInteraction(); // прекращаем IK-анимацию
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("item")){ 
            {
                if (tasks.Task2)
                {
                    TakeItemInPocket(collision.gameObject); 
                }
            else {
                    MainManager.Messenger.WriteMessage("Вы не можете поднять предмет пока не получили задание");
                }
            }
        }

        if (collision.gameObject.CompareTag("itemForTransfer") && !inHand)
        // если это объект для перемещения и в руке нет другого предмета
        {
            if (tasks.Task2)
            {
                TakeItemInHand(collision.gameObject.transform);
            }
            else {
                MainManager.Messenger.WriteMessage("Вы не можете поднять предмет пока не получили задание");
            }
        }
    }

    void TakeItemInPocket(GameObject item)
    {
        this.GetComponent<AudioSource>().Play();   
        playerIK.StopInteraction(); // прекращение IK-анимации
        Destroy(interactObject.gameObject);
        MainManager.Messenger.WriteMessage("Вы подобрали " + item.name + " в инвентарь");
        MainManager.Inventory.AddItem(interactObject.gameObject);
    }

    void TakeItemInHand(Transform item)
    {        
        inHand = item; // запоминаем объект для взаимодействия
        inHand.parent = transform; // делаем руку, родителем объекта
        inHand.localPosition = new Vector3(0, 0, 0); 
        inHand.localEulerAngles = new Vector3(0, 0, 0); 
        playerIK.StopInteraction();
        Destroy(interactObject.GetComponent<Rigidbody>());
        MainManager.Messenger.WriteMessage("Вы подобрали " + item.name);
    }

    void ThroughItem()
    {
        if (inHand != null) // если персонаж держит объект
        {
            inHand.parent = null; // отвязываем объект      
            StartCoroutine(ReadyToTake());
            interactObject.gameObject.AddComponent<Rigidbody>();
        }
    }

    IEnumerator ReadyToTake()
    {
        yield return null;
        inHand = null; 
    }
}
