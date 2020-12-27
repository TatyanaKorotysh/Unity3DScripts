using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    NavMeshAgent botagent;
    Animator animbot;
    [SerializeField]
    GameObject[] points; // массив точек для переходов
    GameObject player;
    float weight = 0;
    public Dialog dialog;
    private Tasks tasks;

    //public static Dialog dialog;

    enum states
    {
        waiting,
        going, 
        dialog,
    }
    states state = states.waiting;

    void Start()
    {
        animbot = GetComponent<Animator>();
        botagent = GetComponent<NavMeshAgent>(); 
        StartCoroutine(Wait()); 
        player = FindObjectOfType<PlayerMove>().gameObject;
        //dialog = GetComponent<Dialog>();
        tasks = GameObject.Find("Canvas").GetComponent<Tasks>();
    }


    void FixedUpdate()
    {
        switch (state)
        {
            case (states.waiting):
            {
                if (PlayerNear()) PrepareToDialog();
                break;
            }
            case states.going:
            {
                if (PlayerNear()) PrepareToDialog();
                else if ((Vector3.Distance(transform.position, botagent.destination)) < 3)
                {
                    StartCoroutine(Wait());
                }
                break;
            }
            case states.dialog:
            {
                if (!PlayerNear()) StartCoroutine(Wait());
                break;
            }
        }

    }

    bool PlayerNear()
    {
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) < 10) return true;
        else return false;
    }

    void PrepareToDialog()
    {
        botagent.SetDestination(transform.position); // обнуляем точку, чтобы бот никуда не шёл
        animbot.SetBool("Walking", false); 
        state = states.dialog;
    }
    

     IEnumerator Wait() 
     {
        botagent.SetDestination(transform.position); // обнуляем точку, чтобы бот никуда не шёл
        animbot.SetBool("Walking", false); 
        //botagent.updateRotation = false;
        state = states.waiting;

        yield return new WaitForSeconds(10f);

        botagent.SetDestination(points[Random.Range(0, points.Length)].transform.position);
        // destination – куда идти боту, передаем ему рандомно одну из наших точек
        animbot.SetBool("Walking", true); // включаем анимацию ходьбы
        state = states.going;
     }


    void OnAnimatorIK()
    {
        if (state == states.dialog)
        {
            if (weight < 1) weight += 0.1f;
            animbot.SetLookAtWeight(weight); // указываем силу воздействия на голову
            animbot.SetLookAtPosition(player.transform.TransformPoint(Vector3.up * 7f));
            // указываем куда смотреть
        }
        else if (weight > 0)
        {
            weight -= 0.1f;
            animbot.SetLookAtWeight(weight);
            animbot.SetLookAtPosition(player.transform.TransformPoint(Vector3.up * 7f));
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            if (tasks.Task1 && tasks.Task2 && tasks.Task3 && tasks.Task4)
            {
                MainManager.Messenger.WriteMessage("Теперь вы можите выйти из леса!");
            }
            else {
                FindObjectOfType<DialogTrigger>().TriggerDialog();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<DialogManager>().StopDialog();
        }
    }
}
